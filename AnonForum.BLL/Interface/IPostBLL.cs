using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.DTOs.User;
using AnonForum.BO;

namespace AnonForum.BLL.Interface
{
    public interface IPostBLL
    {
        void AddNewPost(CreatePostDTO entity);
        void DeletePost(int postID);
        void DislikePost(int postID, int userID);
        void EditPost(EditPostDTO entity);
        IEnumerable<PostCategoryDTO> GetAllCategories();
        IEnumerable<PostDTO> GetAllPosts();
		IEnumerable<PostDTO> GetAllPostsbyCategory(int catID);
		IEnumerable<PostDTO> GetAllPostsbySearch(string query);
		bool GetDislikePost(int postID, int userID);
        bool GetLikePost(int postID, int userID);
        PostDTO GetPostbyID(int postID);
        void LikePost(int postID, int userID);
        void UndislikePost(int postID, int userID);
        void UnlikePost(int postID, int userID);
    }
}
