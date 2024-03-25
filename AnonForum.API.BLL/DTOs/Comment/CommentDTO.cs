using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.API.BLL.DTOs.User;

namespace AnonForum.API.BLL.DTOs.Comment
{
    public class CommentDTO
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string CommentText { get; set; }
        public DateTime TimeStamp { get; set; }
        public int? TotalLikes { get; set; }
        public int? TotalDislikes { get; set; }
        public UserDTO User { get; set; }
	}
}
