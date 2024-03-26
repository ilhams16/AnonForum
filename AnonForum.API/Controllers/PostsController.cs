using AnonForum.API.BLL.DTOs.Post;
using AnonForum.API.BLL.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AnonForum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostBLL _postBLL;
        //private readonly IValidator<CreatePostDTO> _validatorCreatePostDto;
        private readonly IValidator<EditPostDTO> _validatorEditPostDTO;

        public PostsController(IPostBLL postBLL, 
            //IValidator<CreatePostDTO> validatorCreatePostDto,
            IValidator<EditPostDTO> validatorEditPostDTO)
        {
            _postBLL = postBLL;
            //_validatorCreatePostDto = validatorCreatePostDto;
            _validatorEditPostDTO = validatorEditPostDTO;
        }
        // GET: api/<PostsController>
        [HttpGet]
        public async Task<IEnumerable<PostDTO>> Get()
        {
            return await _postBLL.GetAllPosts();
        }

        //[HttpGet("Search")]
        //public async Task<IEnumerable<PostDTO>> GetBySearch([FromQuery(Name = "search")] string query)
        //{
        //    return await _postBLL.GetAllPostsbySearch(query);
        //}

        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public async Task<PostDTO> Get(int id)
        {
            return await _postBLL.GetPostbyID(id);
        }

        [HttpGet("Like/{id}")]
        public async Task<IEnumerable<LikePostDTO>> GetLikes(int id)
        {
            return await _postBLL.GetLikePost(id);
        }

        [HttpGet("Dislike/{id}")]
        public async Task<IEnumerable<DislikePostDTO>> GetDislikes(int id)
        {
            return await _postBLL.GetDislikePost(id);
        }

        [HttpGet("Category/{id}")]
        public async Task<IEnumerable<PostDTO>> GetByCategory(int id)
        {
            return await _postBLL.GetAllPostsbyCategory(id);
        }

        // POST api/<PostsController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CreatePostDTO createPost)
        {
            try
            {
                if (createPost.ImageFilePost != null)
                {
                    var newName = $"{Guid.NewGuid()}_{createPost.ImageFilePost.FileName}";
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PostImages", newName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await createPost.ImageFilePost.CopyToAsync(stream);
                    }
                    createPost.Image = newName;
                }
                await _postBLL.AddNewPost(createPost);
                return CreatedAtAction("Get", createPost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Like")]
        public async Task<IActionResult> LikePost(int userID, int postID)
        {
            try
            {
                await _postBLL.LikePost(postID, userID);
                var users = await _postBLL.GetLikePost(postID);
                return CreatedAtAction("Get", users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Unlike")]
        public async Task<IActionResult> UnlikePost(int userID, int postID)
        {
            try
            {
                await _postBLL.UnlikePost(postID, userID);
                var users = await _postBLL.GetLikePost(postID);
                return CreatedAtAction("Get", users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Dislike")]
        public async Task<IActionResult> DislikePost(int userID, int postID)
        {
            try
            {
                await _postBLL.DislikePost(postID, userID);
                var users = await _postBLL.GetDislikePost(postID);
                return CreatedAtAction("Get", users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Undislike")]
        public async Task<IActionResult> UndislikePost(int userID, int postID)
        {
            try
            {
                await _postBLL.UndislikePost(postID, userID);
                var users = await _postBLL.GetDislikePost(postID);
                return CreatedAtAction("Get", users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PostsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditPostDTO editPost)
        {
            try
            {
                var post = await _postBLL.GetPostbyID(id);
                if (post == null)
                {
                    return NotFound();
                }
                await _postBLL.EditPost(editPost);
                return CreatedAtAction("Get", editPost);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PostsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var post = await _postBLL.GetPostbyID(id);
                if (post == null)
                {
                    return NotFound();
                }
                await _postBLL.DeletePost(id);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message} : {ex.InnerException}");
            }
        }
    }
}
