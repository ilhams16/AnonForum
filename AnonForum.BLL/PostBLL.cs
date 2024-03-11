using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.DTOs.User;
using AnonForum.BLL.Interface;
using AnonForum.BO;
using AnonForum.DAL;

namespace AnonForum.BLL
{
    public class PostBLL : IPostBLL
    {
        private readonly IPost _postDAL;
        public PostBLL()
        {
            _postDAL = new PostDAL();
        }

        public void AddNewPost(CreatePostDTO entity)
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
                    Image = entity.Image,

                };
                _postDAL.AddNewPost(newPost);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public PostDTO GetPostbyID(int postID)
        {
            PostDTO postDto = new PostDTO();
            var post = _postDAL.GetPostbyID(postID);
            if (post != null)
            {
                postDto.PostID = post.PostID;
                postDto.UserID = post.UserID;
                postDto.Title = post.Title;
                postDto.PostText = post.PostText;
                postDto.PostCategoryID = post.PostCategoryID;
                postDto.Image = post.Image;
                postDto.TimeStamp = post.TimeStamp;
                postDto.TotalLikes = post.TotalDislikes;
                postDto.TotalDislikes = post.TotalDislikes;
                postDto.Username = post.Username;
                postDto.UserImage = post.UserImage;
            }
            else
            {
                throw new ArgumentException($"{post.PostID} not found");
            }
            return postDto;
        }
        public void DeletePost(int postID)
        {

            try
            {
                _postDAL.DeletePost(postID);
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
                    PostID = post.PostID,
                    UserID = post.UserID,
                    Title = post.Title,
                    PostText = post.PostText,
                    PostCategoryID = post.PostCategoryID,
                    Image = post.Image,
                    TimeStamp = post.TimeStamp,
                    TotalLikes = post.TotalLikes,
                    TotalDislikes = post.TotalDislikes,
                    Username = post.Username,
                    UserImage = post.UserImage,
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
        public bool GetLikePost(int postID, int userID)
        {
            try
            {
                return (bool)_postDAL.GetLike(postID, userID);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public void LikePost(int postID, int userID)
        {
            _postDAL.LikePost(postID, userID);
        }
        public void UnlikePost(int postID, int userID)
        {
            _postDAL.UnlikePost(postID, userID);
        }
        public bool GetDislikePost(int postID, int userID)
        {
            try
            {
                return (bool)_postDAL.GetDislike(postID, userID);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        public void DislikePost(int postID, int userID)
        {
            _postDAL.DislikePost(postID, userID);
        }
        public void UndislikePost(int postID, int userID)
        {
            _postDAL.UndislikePost(postID, userID);
        }
        public void EditPost(EditPostDTO entity, int postID)
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
                    Title = entity.Title,
                    PostText = entity.PostText,
                    PostCategoryID = entity.PostCategoryID,

                };
                _postDAL.EditPost(postID,newPost);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
