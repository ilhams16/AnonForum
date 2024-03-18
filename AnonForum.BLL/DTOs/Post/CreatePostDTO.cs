using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AnonForum.BLL.DTOs.Post
{
    public class CreatePostDTO
    {
        public string Title { get; set; }
        public string PostText { get; set; }
        public int UserID { get; set; }
        public int PostCategoryID { get; set; }
		public IFormFile ImageFilePost { get; set; }
		public string Image { get; set; }
	}
}
