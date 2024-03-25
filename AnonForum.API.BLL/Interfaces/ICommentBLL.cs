using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.Comment;
using AnonForum.API.BLL.DTOs.Post;

namespace AnonForum.API.BLL.Interfaces
{
    public interface ICommentBLL
    {
        Task<IEnumerable<CommentDTO>> GetAllCommentbyPostID(int postID);
        Task<CommentDTO> AddNewComment(CreateCommentDTO entity);
        Task DeleteComment(int commentID);
        Task<CommentDTO> GetCommentbyUserIDandPostID(int userID, int postID);
        Task<IEnumerable<LikeCommentDTO>> GetLike(int commentID);
        Task<IEnumerable<DislikeCommentDTO>> GetDislike(int commentID);
        Task LikeComment(int commentID, int userID);
        Task UnlikeComment(int commentID, int userID);
        Task DislikeComment(int commentID, int userID);
        Task UndislikeComment(int commentID, int userID);
        Task<CommentDTO> GetCommentbyCommentID(int commentID);
        Task<CommentDTO> EditComment(EditCommentDTO entity);
    }
}
