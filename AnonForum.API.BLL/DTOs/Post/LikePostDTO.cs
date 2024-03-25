using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.API.BLL.DTOs.User;

namespace AnonForum.API.BLL.DTOs.Post
{
    public class LikePostDTO
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public UserDTO User { get; set; }

    }
}
