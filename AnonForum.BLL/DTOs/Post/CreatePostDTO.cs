using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.BLL.DTOs.Post
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string PostText { get; set; }
        public int UserID { get; set; }
        public int PostCategoryID { get; set; }
		public string Image { get; set; }
	}
}
