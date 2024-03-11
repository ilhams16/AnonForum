using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.BLL.DTOs.Post
{
    public class PostDTO
    {
        public int PostID { get; set; }
        public string Title { get; set; }
        public string PostText { get; set; }
        public int UserID { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Image { get; set; }
        public int PostCategoryID { get; set; }
        public int TotalLikes { get; set; }
        public int TotalDislikes { get; set; }
        public string Username { get; set; }
		public string UserImage { get; set; }

	}
}
