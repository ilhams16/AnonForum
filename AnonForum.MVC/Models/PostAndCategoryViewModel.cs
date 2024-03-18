using AnonForum.BLL.DTOs.Comment;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.Interface;
using AnonForum.BO;

namespace AnonForum.MVC.Models
{
	public class PostAndCategoryViewModel
	{
		public List<int>? GetLikes { get; set; }
		public List<int>? GetDislikes { get; set; }
        public List<int>? GetLikesComment { get; set; }
        public List<int>? GetDislikesComment { get; set; }
		public List<string>? PostImages { get; set; }
		public IEnumerable<PostDTO>? Posts { get; set; }
		public IEnumerable<CommentDTO>? Comments { get; set; }
		public IEnumerable<PostCategoryDTO>? Categories { get; set; }
		public CreatePostDTO? createPostDTO { get; set; }
		public int CurrentPage { get; set; }
		public int PageSize { get; set; }
		public int TotalCount { get; set; }
		public int TotalPages { get; set; }
	}
}
