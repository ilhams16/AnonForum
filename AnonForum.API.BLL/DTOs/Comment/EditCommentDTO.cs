using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnonForum.API.BLL.DTOs.Comment
{
    public class EditCommentDTO
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string CommentText { get; set; }
    }
}
