using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnonForum.BLL.DTOs.Comment;
using AnonForum.BLL.DTOs.Post;

namespace AnonForum.MVC.Models
{
    public class DetailsPostViewModel
    {
        public PostDTO? PostDetail { get; set; }
		public IEnumerable<CommentDTO>? CommentsDetails { get; set; }
    }
}