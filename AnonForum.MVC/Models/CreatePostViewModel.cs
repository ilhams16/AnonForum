using AnonForum.BLL.DTOs.Post;

namespace AnonForum.MVC.Models
{
	public class CreatePostViewModel
	{
		public IEnumerable<PostCategoryDTO>? Categories { get; set; }
		public CreatePostDTO? createPostDTO { get; set; }
	}
}
