using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Comment;

namespace AnonForum.BLL.Interface
{
    public interface ICommentBLL
    {
        IEnumerable<CommentDTO> GetAllCommentbyPostID(int postID);
        void AddNewComment(CreateCommentDTO entity);
        CommentDTO GetCommentbyUserIDandPostID(int userID, int postID);
        bool GetLike(int commentID, int postID, int userID);
        bool GetDislike(int commentID, int postID, int userID);
        void LikeComment(int commentID, int postID, int userID);
        void UnlikeComment(int commentID, int postID, int userID);
        void DislikeComment(int commentID, int postID, int userID);
        void UndislikeComment(int commentID, int postID, int userID);
        void DeleteComment(int commentID);
    }
}
