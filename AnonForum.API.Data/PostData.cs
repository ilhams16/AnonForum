using System.ComponentModel.Design;
using AnonForum.API.Data.Interfaces;
using AnonForum.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Data
{
    public class PostData : IPostData
    {
        private readonly AppDbContext _context;
        public PostData(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Post> AddNewPost(Post post)
        {
            try
            {
                await _context.ExecuteSqlAsync("EXEC [dbo].[NewPost] {0}, {1}, {2}, {3}, {4}", post.UserId, post.Title, post.PostText, post.PostCategoryId, post.Image);
                await _context.SaveChangesAsync();
                return post;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }

        public async Task DeletePost(int postID)
        {
            var post = await _context.Posts.FindAsync(postID);
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }
            await _context.ExecuteSqlAsync("EXEC [dbo].[DeletePost] {0}", postID);
            await _context.SaveChangesAsync();
        }

        public async Task DislikePost(int postID, int userID)
        {
            await _context.ExecuteSqlAsync("EXEC [dbo].[UnlikePostSP] {0}, {1}", userID, postID);
            await _context.SaveChangesAsync();
        }

        public async Task<Post> EditPost(int postID, Post newPost)
        {
            var post = await _context.Posts.Where(p => p.PostId == postID).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }
            post.Title = newPost.Title;
            post.PostText = newPost.PostText;
            post.PostCategory = newPost.PostCategory;
            await _context.SaveChangesAsync();
            return post;
        }

        public async Task<IEnumerable<Post>> GetAllPost()
        {
            var posts = await _context.Posts.Include(p => p.PostCategory).Include(p => p.Comments).Include(p=>p.User).OrderByDescending(p => p.TimeStamp).ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetAllPostbyCategories(int catID)
        {
            var posts = await _context.Posts.Include(p => p.PostCategory).Where(p => p.PostCategoryId == catID).Include(p=>p.User).ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<Post>> GetAllPostbySearch(string query)
        {
            var posts = await _context.Posts.Include(p => p.PostCategory).Include(p => p.Comments).Include(p => p.User).Include(p => p.LikePosts).Where(p => p.Title == query).ToListAsync();
            return posts;
        }

        public async Task<IEnumerable<UnlikePost>> GetDislike(int postID)
        {
            var post = await _context.UnlikePosts.Where(p => p.PostId == postID).Include(p=>p.User).ToListAsync();
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }
            return post;
        }

        public async Task<IEnumerable<LikePost>> GetLike(int postID)
        {
            var post = await _context.LikePosts.Where(p => p.PostId == postID).Include(p => p.User).ToListAsync();
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }
            return post;
        }

        public async Task<Post> GetPostbyID(int postID)
        {
            var post = await _context.Posts.Include(p => p.PostCategory).Include(p => p.Comments).Include(p => p.UnlikePosts).Include(p => p.LikePosts).Where(p => p.PostId == postID).FirstOrDefaultAsync();
            return post;
        }

        public async Task LikePost(int postID, int userID)
        {
            await _context.ExecuteSqlAsync("EXEC [dbo].[LikePostSP] {0}, {1}", userID, postID);
            await _context.SaveChangesAsync();
        }

        public async Task UndislikePost(int postID, int userID)
        {
            var post = await _context.UnlikePosts.Where(p => p.PostId == postID && p.UserId == userID).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }
            _context.UnlikePosts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task UnlikePost(int postID, int userID)
        {
            var post = await _context.LikePosts.Where(p => p.PostId == postID && p.UserId == userID).FirstOrDefaultAsync();
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }
            _context.LikePosts.Remove(post);
            await _context.SaveChangesAsync();
        }
    }
}
