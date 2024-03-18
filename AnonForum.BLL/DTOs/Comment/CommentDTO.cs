using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.BLL.DTOs.Comment
{
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? TotalLikes { get; set; }
        public int? TotalDislikes { get; set; }
        public string Username { get; set; }
		public string UserImage { get; set; }
	}
}
