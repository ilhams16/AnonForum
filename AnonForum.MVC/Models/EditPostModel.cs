using AnonForum.BLL.DTOs.Post;

namespace AnonForum.MVC.Models
{
	public class EditPostModel
	{
		public IEnumerable<PostCategoryDTO>? Categories { get; set; }
		public EditPostDTO? EditPostDTO { get; set; }
	}
}
