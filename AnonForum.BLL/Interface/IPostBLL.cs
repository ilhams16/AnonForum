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
        string AddNewPost(CreatePostDTO entity);
        string DeletePost(string title, int userID);
        void DislikePost(int postID, int userID);
        IEnumerable<PostCategoryDTO> GetAllCategories();
        IEnumerable<PostDTO> GetAllPosts();
        bool GetDislikePost(int postID, int userID);
        bool GetLikePost(int postID, int userID);
        PostDTO GetPostbyTitleandUsername(string title, string username);
        void LikePost(int postID, int userID);
        void UndislikePost(int postID, int userID);
        void UnlikePost(int postID, int userID);
    }
}
