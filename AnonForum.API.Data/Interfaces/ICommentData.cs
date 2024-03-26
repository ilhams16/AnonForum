using AnonForum.API.Domain;

namespace AnonForum.API.Data.Interfaces
{
    public interface ICommentData
    {
        public Task LikeComment(int commentID, int userID, int postID);
        public Task UnlikeComment(int commentID, int userID);
        public Task DislikeComment(int commentID, int userID, int postID);
        public Task UndislikeComment(int commentID, int userID);
        public Task<Comment> AddNewComment(Comment comment);
        public Task<IEnumerable<Comment>> GetAllCommentbyPostID(int postID);
        public Task<IEnumerable<LikeComment>> GetLike(int commentID);
        public Task<IEnumerable<UnlikeComment>> GetDislike(int commentID);
        public Task DeleteComment(int commentID);
        public Task<Comment> GetCommentbyUserIDandPostID(int userID, int postID);
        public Task<Comment> GetCommentbyCommentID(int commentID);
        public Task<Comment> EditComment(int commentID, Comment newComment);
    }
}
