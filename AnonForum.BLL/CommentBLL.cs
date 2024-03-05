using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Comment;
using AnonForum.BLL.DTOs.Post;
using AnonForum.DAL;
using CommentDTO = AnonForum.BLL.DTOs.Comment.CommentDTO;

namespace AnonForum.BLL
{
    public class CommentBLL
    {
        private readonly IComment _commentDAL;
        public CommentBLL()
        {
            _commentDAL = new CommentDAL();
        }
        public IEnumerable<CommentDTO> GetCommentbyID(int postID)
        {
            List<CommentDTO> listCommentsDto = new List<CommentDTO>();
            var comments = _commentDAL.GetAllCommentbyPostID(postID);
            foreach (var comment in comments)
            {
                listCommentsDto.Add(new CommentDTO
                {
                    CommentID = comment.CommentID,
                    UserID = comment.UserID,
                    PostID = comment.PostID,
                    Comment = comment.Comment,
                    TotalLikes = comment.TotalLikes,
                    TotalDislikes = comment.TotalDislikes,
                    Username = comment.Username,
                });
            }
            return listCommentsDto;
        }
    }
}
