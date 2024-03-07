using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Comment;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.Interface;
using AnonForum.BO;
using AnonForum.DAL;
using CommentDTO = AnonForum.BLL.DTOs.Comment.CommentDTO;

namespace AnonForum.BLL
{
    public class CommentBLL : ICommentBLL
    {
        private readonly IComment _commentDAL;
        public CommentBLL()
        {
            _commentDAL = new CommentDAL();
        }
        public IEnumerable<CommentDTO> GetAllCommentbyPostID(int postID)
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
        public void AddNewComment(CreateCommentDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Comment))
            {
                throw new ArgumentException("Comment is required");
            }

            try
            {
                var newComment = new Comment
                {
                    PostID = entity.PostID,
                    UserID = entity.UserID,
                    Comment = entity.Comment,

                };
                _commentDAL.AddNewComment(newComment);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void DeleteComment(int commentID, int postID, int userID)
        {
            try
            {
                _commentDAL.DeleteComment(commentID, postID, userID);
            }
            catch (Exception ex)
            { throw new ArgumentException(ex.Message); }
        }

        public CommentDTO GetCommentbyUserIDandPostID(int userID, int postID)
        {
            try
            {
                CommentDTO comDto = new CommentDTO();
                var com = _commentDAL.GetCommentbyUserIDandPostID(userID,postID);
                if (com != null)
                {
                    comDto.CommentID = com.CommentID;
                    comDto.PostID = com.PostID;
                    comDto.UserID = com.UserID;
                    comDto.Comment = com.Comment;
                    comDto.TotalLikes = com.TotalDislikes;
                    comDto.TotalDislikes = com.TotalDislikes;
                    comDto.Username = com.Username;
                }
                else
                {
                    throw new ArgumentException($"{com.PostID} not found");
                }
                return comDto;
            }
            catch (Exception ex)
            {
                throw new ArgumentException (ex.Message);
            }
        }

        public bool GetLike(int commentID, int postID, int userID)
        {
            try
            {
                bool res = _commentDAL.GetLike(commentID, postID, userID);
                return res;
            } catch (Exception ex)
            {
                throw new ArgumentException (ex.Message);
            }
        }

        public bool GetDislike(int commentID, int postID, int userId)
        {
            try
            {
                return (bool)_commentDAL.GetDislike(commentID, postID, userId); ;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void LikeComment(int commentID, int postID, int userID)
        {
            try
            {
                _commentDAL.LikeComment(commentID, postID, userID); 
            } catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void UnlikeComment(int commentID, int postID, int userID)
        {
            try
            {
                _commentDAL.UnlikeComment(commentID, postID, userID);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void DislikeComment(int commentID, int postID, int userID)
        {
            try
            {
                _commentDAL.DislikeComment(commentID, postID, userID);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public void UndislikeComment(int commentID, int postID, int userID)
        {
            try
            {
                _commentDAL.UndislikeComment(commentID, postID, userID);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
