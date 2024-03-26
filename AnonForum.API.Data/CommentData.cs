using AnonForum.API.Data.Interfaces;
using AnonForum.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Data
{
    public class CommentData : ICommentData
    {
        private readonly AppDbContext _context;
        public CommentData(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Comment> AddNewComment(Comment comment)
        {
            try
            {
                await _context.ExecuteSqlAsync("EXEC [dbo].[NewComment] {0}, {1}, {2}", comment.PostId, comment.UserId, comment.CommentText);
                await _context.SaveChangesAsync();
                return comment;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }

        public async Task<Comment> EditComment(int commentID, Comment newComment)
        {
            var comment = await _context.Comments.Where(c => c.CommentId == commentID).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new ArgumentException("Post not found");
            }
            comment.CommentText = newComment.CommentText;
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task DeleteComment(int commentID)
        {
            var comment = await _context.Comments.FindAsync(commentID);
            if (comment == null)
            {
                throw new ArgumentException("Comment not found");
            }
            await _context.ExecuteSqlAsync("EXEC [dbo].[DeleteComment] {0}",commentID);
            await _context.SaveChangesAsync();
        }

        public async Task DislikeComment(int commentID, int userID, int postID)
        {
            await _context.ExecuteSqlAsync("EXEC [dbo].[UnlikeCommentSP] {0}, {1}, {2}", commentID, userID, postID);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllCommentbyPostID(int postID)
        {
            var comments = await _context.Comments.Include(c => c.User).Where(c => c.PostId == postID).OrderByDescending(c => c.TimeStamp).ToListAsync();
            return comments;
        }

        public async Task<Comment> GetCommentbyUserIDandPostID(int userID, int postID)
        {
            var comment = await _context.Comments.Include(c => c.User).Where(c => c.PostId == postID && c.UserId == userID).OrderByDescending(c => c.TimeStamp).FirstOrDefaultAsync();
            return comment;
        }

        public async Task<Comment> GetCommentbyCommentID(int commentID)
        {
            var comment = await _context.Comments.Include(c => c.User).Where(c => c.CommentId == commentID).OrderByDescending(c => c.TimeStamp).FirstOrDefaultAsync();
            return comment;
        }

        public async Task<IEnumerable<UnlikeComment>> GetDislike(int commentID)
        {
            var post = await _context.UnlikeComments.Where(p => p.CommentId==commentID).Include(p => p.User).ToListAsync();
            if (post == null)
            {
                throw new ArgumentException("Comment not found");
            }
            return post;
        }

        public async Task<IEnumerable<LikeComment>> GetLike(int commentID)
        {
            var post = await _context.LikeComments.Where(p => p.CommentId == commentID).Include(p => p.User).ToListAsync();
            if (post == null)
            {
                throw new ArgumentException("Comment not found");
            }
            return post;
        }

        public async Task LikeComment(int commentID, int userID, int postID)
        {
            await _context.ExecuteSqlAsync("EXEC [dbo].[LikeCommentSP] {0}, {1}, {2}", commentID, userID, postID);
            await _context.SaveChangesAsync();
        }

        public async Task UndislikeComment(int commentID, int userID)
        {
            var comment = await _context.UnlikeComments.Where(c => c.CommentId == commentID && c.UserId == userID).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new ArgumentException("Comment not found");
            }
            _context.UnlikeComments.Remove(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UnlikeComment(int commentID, int userID)
        {
            var comment = await _context.LikeComments.Where(c => c.CommentId == commentID && c.UserId == userID).FirstOrDefaultAsync();
            if (comment == null)
            {
                throw new ArgumentException("Comment not found");
            }
            _context.LikeComments.Remove(comment);
            await _context.SaveChangesAsync();
        }
    }
}
