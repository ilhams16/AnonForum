using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using AnonForum.BLL;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.DTOs.User;
using AnonForum.BLL.Interface;
using AnonForum.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnonForum.MVC.Controllers
{
	public class HomeController : Controller
	{
		private readonly IPostBLL _postBLL;
		private readonly IUserBLL _userBLL;

		public HomeController(IPostBLL postBLL, IUserBLL userBLL)
		{
			_postBLL = postBLL;
			_userBLL = userBLL;
		}

		public IActionResult Index(string? search, int? category)
		{
			var categories = _postBLL.GetAllCategories();
			PostAndCategoryViewModel postAndCategoryViewModel = new PostAndCategoryViewModel();
			postAndCategoryViewModel.Categories = categories;
			var models = _postBLL.GetAllPosts();
			if (category != null)
			{
				models = _postBLL.GetAllPostsbyCategory((int)category);
				postAndCategoryViewModel.Posts = models;
				return View(postAndCategoryViewModel);
			}
			else if (search != null)
			{
				models = _postBLL.GetAllPostsbySearch(search);
				postAndCategoryViewModel.Posts = models;
				return View(postAndCategoryViewModel);
			}
			else if (search == null || category == null || category == 0)
			{
				models = _postBLL.GetAllPosts();
				postAndCategoryViewModel.Posts = models;
				return View(postAndCategoryViewModel);
			}
			else
			{
				return View(postAndCategoryViewModel);
			}
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
					string imagePath = SaveImage(userDTO.ImageFile);

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
		private string SaveImage(IFormFile file)
		{
			if (file != null && file.Length > 0)
			{
				try
				{
					string fileExtension = Path.GetExtension(file.FileName);
					string uniqueFileName = $"{Guid.NewGuid().ToString()}{fileExtension}";

					string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", "UserImages");
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

				catch (Exception)
				{
					// Handle error
					throw;
				}
			}
			return null;
		}
	}
}
