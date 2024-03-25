using AnonForum.API.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AnonForum.API.Data.Interfaces
{
    public interface IPostData
    {
        public Task<Post> AddNewPost(Post post);
        public Task<IEnumerable<Post>> GetAllPost();
        public Task LikePost(int postID, int userID);
        public Task UnlikePost(int postID, int userID);
        public Task DislikePost(int postID, int userID);
        public Task UndislikePost(int postID, int userID);
        public Task DeletePost(int postID);
        public Task<Post> EditPost(int postID, Post newPost);
        public Task<Post> GetPostbyID(int postID);
        public Task<IEnumerable<Post>> GetAllPostbyCategories(int catID);
        public Task<IEnumerable<Post>> GetAllPostbySearch(string query);
        Task<IEnumerable<LikePost>> GetLike(int postID);
        Task<IEnumerable<UnlikePost>> GetDislike(int postID);
    }
}
