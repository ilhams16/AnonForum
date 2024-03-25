using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.Post;

namespace AnonForum.API.BLL.Interfaces
{
    public interface IPostBLL
    {
        Task<PostDTO> AddNewPost(CreatePostDTO entity);
        Task DeletePost(int postID);
        Task DislikePost(int postID, int userID);
        Task<PostDTO> EditPost(EditPostDTO entity);
        Task<IEnumerable<PostCategoryDTO>> GetAllCategories();
        Task<IEnumerable<PostDTO>> GetAllPosts();
        Task<IEnumerable<PostDTO>> GetAllPostsbyCategory(int catID);
        Task<IEnumerable<PostDTO>> GetAllPostsbySearch(string query);
        Task<IEnumerable<DislikePostDTO>> GetDislikePost(int postID);
        Task<IEnumerable<LikePostDTO>> GetLikePost(int postID);
        Task<PostDTO> GetPostbyID(int postID);
        Task LikePost(int postID, int userID);
        Task UndislikePost(int postID, int userID);
        Task UnlikePost(int postID, int userID);
    }
}
