using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using AnonForum.BLL;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.DTOs.User;
using AnonForum.BLL.Interface;
using AnonForum.DAL;
using AnonForum.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AnonForum.BLL.DTOs.Comment;

namespace AnonForum.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPostBLL _postBLL;
		private readonly ICommentBLL _commentBLL;
		private readonly IUserBLL _userBLL;
		private readonly ILogger<HomeController> _logger;

		public HomeController(IPostBLL postBLL, IUserBLL userBLL, ICommentBLL commentBLL, ILogger<HomeController> logger)
		{
			_postBLL = postBLL;
			_userBLL = userBLL;
			_commentBLL = commentBLL;
			_logger = logger;
		}

		public IActionResult Index(string search, int? category, int page = 1, int pageSize = 5)
		{
			_logger.LogInformation("Index action method called.");

			UserDTO userDto = new UserDTO();
			if (HttpContext.Session.GetString("userLogin") != null)
			{
				var user = HttpContext.Session.GetString("userLogin");
				userDto = JsonSerializer.Deserialize<UserDTO>(user);
			}

			var categories = _postBLL.GetAllCategories();

			PostAndCategoryViewModel postAndCategoryViewModel = new PostAndCategoryViewModel();
			postAndCategoryViewModel.Categories = categories;

			IEnumerable<PostDTO> models;

			if (!string.IsNullOrEmpty(search) && category != null && category != 0)
			{
				var bySearch = _postBLL.GetAllPostsbySearch(search);
				models = bySearch.Where(p => p.PostCategoryID == category);
			}
			else if (!string.IsNullOrEmpty(search))
			{
				models = _postBLL.GetAllPostsbySearch(search);
			}
			else if (category != null && category != 0)
			{
				models = _postBLL.GetAllPostsbyCategory((int)category);
			}
			else
			{
				models = _postBLL.GetAllPosts();
			}

			// Calculate total number of pages
			int totalPosts = models.Count();
			int totalPages = (int)Math.Ceiling((double)totalPosts / pageSize);

			// Retrieve data for the current page
			models = models.Skip((page - 1) * pageSize).Take(pageSize);
			List<string> images = new List<string>();

			foreach (var post in models)
			{
				var comments = _commentBLL.GetAllCommentbyPostID(post.PostID).ToList();
				foreach (var comment in comments)
				{
					postAndCategoryViewModel.GetLikesComment = comment.Likes;
					postAndCategoryViewModel.GetDislikesComment = comment.Dislikes;
				}
				postAndCategoryViewModel.Comments = comments;
				postAndCategoryViewModel.GetLikes = post.Likes;
				postAndCategoryViewModel.GetDislikes = post.Dislikes;
				images.Add(post.Image);
			}
			postAndCategoryViewModel.PostImages = images;
			postAndCategoryViewModel.Posts = models;
			postAndCategoryViewModel.CurrentPage = page;
			postAndCategoryViewModel.PageSize = pageSize;
			postAndCategoryViewModel.TotalCount = totalPosts;
			postAndCategoryViewModel.TotalPages = totalPages;

			return View(postAndCategoryViewModel);
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Login(UserLoginDTO userLogin)
		{
			try
			{
				if (ModelState.IsValid)
				{
					// Check login logic here (e.g., authenticate user)
					if (_userBLL.UserLogin(userLogin) is not null)
					{
						UserDTO userDto = _userBLL.UserLogin(userLogin);
						//simpan username ke session
						var userDtoSerialize = JsonSerializer.Serialize(userDto);
						HttpContext.Session.SetString("userLogin", userDtoSerialize);
						// Redirect authenticated user to dashboard or home page
						return RedirectToAction("Index", "Home");
					}
					else
					{
						// If authentication fails, add model error
						ModelState.AddModelError("", "Invalid username or password.");
					}
				}
				// If model state is not valid, return to login view with errors
				return View(userLogin);
			}

			catch (Exception ex)
			{
				ViewData["ErrorMessage"] = $"{ex.Message}";
				return View();
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateUserDTO userDTO)
		{
			if (ModelState.IsValid)
			{
				try
				{
					// Save image and get its URL
					string imagePath = SaveImage(userDTO.ImageFile, "UserImages");

					// Set image URL in DTO
					userDTO.UserImage = imagePath;

					// Create user
					_userBLL.AddNewUser(userDTO);


					return RedirectToAction("Index", "Home");
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", $"{ex.Message}");
				}
			}
			// If model state is not valid, return to create user view with errors
			return View(userDTO);
		}
		private string SaveImage(IFormFile file, string location)
		{
			if (file != null && file.Length > 0)
			{
				try
				{
					string fileExtension = Path.GetExtension(file.FileName);
					string uniqueFileName = $"{Guid.NewGuid().ToString()}{fileExtension}";

					string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", location);
					string imagePath = Path.Combine(directoryPath, uniqueFileName);

					// Create directory if it doesn't exist
					if (!Directory.Exists(directoryPath))
					{
						Directory.CreateDirectory(directoryPath);
					}

					// Save image to server
					using (var stream = new FileStream(imagePath, FileMode.Create))
					{
						file.CopyTo(stream);
					}

					// Return image URL
					return uniqueFileName;
				}

				catch (Exception ex)
				{
					// Handle error
					throw new ArgumentException(ex.Message);
				}
			}
			return null;
		}
		public ActionResult Details(int id)
		{
			var post = _postBLL.GetPostbyID(id);
			var comments = _commentBLL.GetAllCommentbyPostID(id);
			var models = new DetailsPostViewModel()
			{
				PostDetail = post,
				CommentsDetails = comments
			};
			return View(models);
		}
		public ActionResult Logout()
		{
			HttpContext.Session.Clear();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Like(int currentPostID, int currentUserID)
		{
			_postBLL.LikePost(currentPostID, currentUserID);

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}

		[HttpPost]
		public IActionResult Unlike(int currentPostID, int currentUserID)
		{
			_postBLL.UnlikePost(currentPostID, currentUserID);

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}

		[HttpPost]
		public IActionResult Dislike(int currentPostID, int currentUserID)
		{
			_postBLL.DislikePost(currentPostID, currentUserID);
			

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}

		[HttpPost]
		public IActionResult Undislike(int currentPostID, int currentUserID)
		{
			_postBLL.UndislikePost(currentPostID, currentUserID);

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}
		[HttpPost]
		public IActionResult LikeComment(int currentCommentID, int currentPostID, int currentUserID)
		{
			_commentBLL.LikeComment(currentCommentID, currentPostID, currentUserID);

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}

		[HttpPost]
		public IActionResult UnlikeComment(int currentCommentID, int currentPostID, int currentUserID)
		{
			_commentBLL.UnlikeComment(currentCommentID, currentPostID, currentUserID);

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}

		[HttpPost]
		public IActionResult DislikeComment(int currentCommentID, int currentPostID, int currentUserID)
		{
			_commentBLL.DislikeComment(currentCommentID, currentPostID, currentUserID);

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}

		[HttpPost]
		public IActionResult UndislikeComment(int currentCommentID, int currentPostID, int currentUserID)
		{
			_commentBLL.UndislikeComment(currentCommentID, currentPostID, currentUserID);

			// Optionally, you can return some result or status here
			return RedirectToAction("Index"); // Redirect to some other action after liking the post
		}
		[HttpPost]
		public ActionResult CreatePost(CreatePostDTO createPostDTO)
		{
			try
			{
				UserDTO userDto = new UserDTO();
				if (HttpContext.Session.GetString("userLogin") != null)
				{
					var user = HttpContext.Session.GetString("userLogin");
					userDto = JsonSerializer.Deserialize<UserDTO>(user);
				}
				string imagePath = string.Empty;
				if (createPostDTO.ImageFilePost != null)
				{

					imagePath = SaveImage(createPostDTO.ImageFilePost, "PostImages");
				}
				// Set image URL in DTO
				createPostDTO.Image = imagePath;
				createPostDTO.UserID = userDto.UserID;
				_postBLL.AddNewPost(createPostDTO);
				TempData["Message"] = "Post successfully";

				// Optionally, you can return some result or status here
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}
			// Redirect to some other action after liking the post
		}
		[HttpPost]
		public ActionResult CreateComment(CreateCommentDTO createCommentDTO)
		{
			try
			{
				UserDTO userDto = new UserDTO();
				if (HttpContext.Session.GetString("userLogin") != null)
				{
					var user = HttpContext.Session.GetString("userLogin");
					userDto = JsonSerializer.Deserialize<UserDTO>(user);
				}
				createCommentDTO.UserID = userDto.UserID;
				_commentBLL.AddNewComment(createCommentDTO);
				TempData["Message"] = "Comment successfully";

				// Optionally, you can return some result or status here
				return RedirectToAction("Index");
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}
			// Redirect to some other action after liking the post
		}
		[HttpPost]
		public ActionResult EditPost(EditPostModel editPostModel)
		{
			try
			{
				// Edit Post
				_postBLL.EditPost(editPostModel.EditPostDTO);
				TempData["Message"] = "Post edited successfully";

				return RedirectToAction("Index", "Home");
			}
			catch (Exception ex)
			{
				throw new ArgumentException(ex.Message);
			}
		}


		[HttpPost]
		public ActionResult DeletePost(int postID)
		{
			_postBLL.DeletePost(postID);
			TempData["Message"] = "Post deleted successfully";
			return RedirectToAction("Index");
		}
		[HttpPost]
		public ActionResult DeleteComment(int commentID)
		{
			_commentBLL.DeleteComment(commentID);
			TempData["Message"] = "Comment deleted successfully";
			return RedirectToAction("Index");
		}
		public ActionResult ConfirmDeleteModal(PostDTO post)
		{
			// Create a new instance of PostDTO and set the PostID

			// Pass the PostDTO to the partial view
			return PartialView("_ConfirmDeleteModal", post);
		}
		public ActionResult ConfirmDeleteCommentModal(CommentDTO comment)
		{
			// Create a new instance of PostDTO and set the PostID

			// Pass the PostDTO to the partial view
			return PartialView("_ConfirmDeleteCommentModal", comment);
		}
		public ActionResult AddNewPostModal(PostCategoryDTO Categories)
		{
			CreatePostViewModel createPostViewModel = new CreatePostViewModel();
			createPostViewModel.Categories = (IEnumerable<PostCategoryDTO>?)Categories;
			// Pass the PostDTO to the partial view
			return PartialView("_AddNewPostModal", createPostViewModel);
		}
		public ActionResult AddCommentModal()
		{
			//CreateCommentDTO createCommentDTO = new CreateCommentDTO() { PostID = PostID };
			//createCommentDTO.PostID = postId;
			// Pass the PostDTO to the partial view
			return PartialView("_AddCommentModal");
		}
		public ActionResult EditPostModal()
		{
			//CreateCommentDTO createCommentDTO = new CreateCommentDTO() { PostID = PostID };
			//createCommentDTO.PostID = postId;
			//Pass the PostDTO to the partial view
			EditPostModel editPostViewModel = new EditPostModel();
			editPostViewModel.Categories = _postBLL.GetAllCategories();
			return PartialView("_EditPostModal", editPostViewModel);
		}
	}
}
