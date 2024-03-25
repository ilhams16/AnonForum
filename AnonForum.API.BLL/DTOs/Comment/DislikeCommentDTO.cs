using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.User;

namespace AnonForum.API.BLL.DTOs.Comment
{
    public class DislikeCommentDTO
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }
    }
}
