using System;
using System.Collections.Generic;
using System.Text;

namespace AnonForum.BLL.DTOs.Comment
{
    public class CreateCommentDTO
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
    }
}
