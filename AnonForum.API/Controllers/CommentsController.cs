using AnonForum.API.BLL.DTOs.Comment;
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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentBLL _commentBLL;
        private readonly IValidator<CreateCommentDTO> _validatorCreateCommentDto;
        private readonly IValidator<EditCommentDTO> _validatorEditCommentDTO;

        public CommentsController(ICommentBLL commentBLL, IValidator<CreateCommentDTO> validatorCreateCommentDto,
            IValidator<EditCommentDTO> validatorEditCommentDTO)
        {
            _commentBLL = commentBLL;
            _validatorCreateCommentDto = validatorCreateCommentDto;
            _validatorEditCommentDTO = validatorEditCommentDTO;
        }

        [HttpGet("Like/{id}")]
        public async Task<IEnumerable<LikeCommentDTO>> GetLikes(int id)
        {
            return await _commentBLL.GetLike(id);
        }

        [HttpGet("Dislike/{id}")]
        public async Task<IEnumerable<DislikeCommentDTO>> GetDislikes(int id)
        {
            return await _commentBLL.GetDislike(id);
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<CommentDTO>> Get(int id)
        {
            return await _commentBLL.GetAllCommentbyPostID(id);
        }

        // POST api/<CommentsController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCommentDTO createComment)
        {
            try
            {
                await _commentBLL.AddNewComment(createComment);
                return CreatedAtAction("Get", createComment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Like")]
        public async Task<IActionResult> LikeComment(int commentID, int userID)
        {
            try
            {
                await _commentBLL.LikeComment(commentID, userID);
                return CreatedAtAction("Get", commentID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Unlike")]
        public async Task<IActionResult> UnlikePost(int commentID, int userID)
        {
            try
            {
                await _commentBLL.UnlikeComment(commentID, userID);
                return CreatedAtAction("Get", commentID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Dislike")]
        public async Task<IActionResult> DislikePost(int commentID, int userID)
        {
            try
            {
                await _commentBLL.DislikeComment(commentID, userID);
                return CreatedAtAction("Get", commentID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("Undislike")]
        public async Task<IActionResult> UndislikePost(int commentID, int userID)
        {
            try
            {
                await _commentBLL.UndislikeComment(commentID, userID);
                return CreatedAtAction("Get", commentID);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CommentsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EditCommentDTO editComment)
        {
            try
            {
                var post = await _commentBLL.GetCommentbyCommentID(id);
                if (post == null)
                {
                    return NotFound();
                }
                await _commentBLL.EditComment(editComment);
                return CreatedAtAction("Get", editComment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CommentsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var post = await _commentBLL.GetCommentbyCommentID(id);
                if (post == null)
                {
                    return NotFound();
                }
                await _commentBLL.DeleteComment(id);
                return Ok("Delete Successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
