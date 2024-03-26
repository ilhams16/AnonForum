using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.API.BLL.DTOs.Comment
{
    public class CreateCommentDTO
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string CommentText { get; set; }
    }
}
