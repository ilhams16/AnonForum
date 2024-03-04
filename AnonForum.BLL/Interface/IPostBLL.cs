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
        IEnumerable<Post> GetPosts();
        IEnumerable<Post> GetPostbyCategory(int id);
        string DeleteUser(string title, int userID);
        Boolean LikePost(int userID, int postID);
    }
}
