using AnonForum.BLL.DTOs.Post;

namespace AnonForum.MVC.Models
{
	public class PostAndCategoryViewModel
	{
		public IEnumerable<PostDTO> Posts { get; set; }
		public IEnumerable<PostCategoryDTO> Categories { get; set; }
	}
}
