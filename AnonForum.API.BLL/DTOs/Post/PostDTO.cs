using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.API.BLL.DTOs.Comment;
using AnonForum.API.BLL.DTOs.User;

namespace AnonForum.API.BLL.DTOs.Post
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
        public PostCategoryDTO PostCategory { get; set; }
        public UserDTO User { get; set; }
        public IEnumerable<CommentDTO> Comments { get; set; }

	}
}
