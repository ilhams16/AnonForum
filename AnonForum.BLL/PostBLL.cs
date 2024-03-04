using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.DTOs.User;
using AnonForum.BO;
using AnonForum.DAL;

namespace AnonForum.BLL
{
    public class PostBLL
    {
        private readonly IPost _postDAL;
        public PostBLL()
        {
            _postDAL = new PostDAL();
        }

        public string AddNewPost(CreatePostDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Title))
            {
                throw new ArgumentException("Title is required");
            }
            else if (entity.Title.Length > 50)
            {
                throw new ArgumentException("Title max length is 50");
            }

            try
            {
                var newPost = new Post
                {
                    UserID = entity.UserID,
                    Title = entity.Title,
                    PostText = entity.PostText,
                    PostCategoryID = entity.PostCategoryID,

                };
                return (string)_postDAL.AddNewPost(newPost);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public string DeletePost(string title, int userID)
        {
            if (title.Length <= 0)
            {
                throw new ArgumentException("Title is required");
            }

            try
            {
                return (string)_postDAL.DeletePost(title,userID);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public IEnumerable<PostDTO> GetAllPosts()
        {
            List<PostDTO> listPostsDto = new List<PostDTO>();
            var posts = _postDAL.GetAllPost();
            foreach (var post in posts)
            {
                listPostsDto.Add(new PostDTO
                {
                    UserID = post.UserID,
                    Title = post.Title,
                    PostText = post.PostText,
                    PostCategoryID = post.PostCategoryID,
                    TotalLikes = post.TotalLikes,
                    TotalDislikes = post.TotalDislikes,
                    Username = post.Username,
                });
            }
            return listPostsDto;
        }

        public IEnumerable<UserDTO> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<PostCategoryDTO> GetAllCategories()
        {
            List<PostCategoryDTO> listCategoriesDto = new List<PostCategoryDTO>();
            var categories = _postDAL.GetAllCategories();
            foreach (var category in categories)
            {
                listCategoriesDto.Add(new PostCategoryDTO
                {
                    PostCategoryID = category.PostCategoryID,
                    Name = category.Name,
                });
            }
            return listCategoriesDto;
        }
        public PostDTO GetPostbyTitleandUsername(string title, string username)
        {
            PostDTO postDto = new PostDTO();
            var post = _postDAL.GetPostbyTitleandUsername(title,username);
            if (post != null)
            {
                postDto.UserID = post.UserID;
                postDto.Title = post.Title;
                postDto.PostText = post.PostText;
                postDto.PostCategoryID = post.PostCategoryID;
                postDto.TotalLikes = post.TotalLikes;
                postDto.TotalDislikes = post.TotalDislikes;
                postDto.Username = post.Username;
            }
            else
            {
                throw new ArgumentException($"Post not found");
            }
            return postDto;
        }
        public bool LikePost(int userID, int postID)
        {
            try
            {
                bool result;
                int like = (int)_postDAL.LikePost(userID, postID);
                if (like != 0) {
                    result = true;
                } else
                {
                    result = false;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
