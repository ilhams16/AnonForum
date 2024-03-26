using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.Post;
using AnonForum.API.BLL.Interfaces;
using AnonForum.API.Data;
using AnonForum.API.Data.Interfaces;
using AnonForum.API.Domain;
using AutoMapper;

namespace AnonForum.API.BLL
{
    public class PostBLL : IPostBLL
    {
        private readonly IPostData _postData;
        private readonly IMapper _mapper;

        public PostBLL(IPostData postData, IMapper mapper)
        {
            _postData = postData;
            _mapper = mapper;
        }

        public async Task<PostDTO> AddNewPost(CreatePostDTO entity)
        {
            try
            {
                var post = _mapper.Map<Post>(entity);
                var result = await _postData.AddNewPost(post);
                var postDto = _mapper.Map<PostDTO>(result);
                return postDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeletePost(int postID)
        {
            try
            {
                await _postData.DeletePost(postID);
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message} : {ex.InnerException}");
            }
        }

        public async Task DislikePost(int postID, int userID)
        {
            try
            {
                await _postData.DislikePost(postID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PostDTO> EditPost(EditPostDTO entity)
        {
            try
            {
                var post = _mapper.Map<Post>(entity);
                await _postData.EditPost(entity.PostID,post);
                var postDto = _mapper.Map<PostDTO>(post);
                return postDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<IEnumerable<PostCategoryDTO>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PostDTO>> GetAllPosts()
        {
            var posts = await _postData.GetAllPost();
            var postsDto = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return postsDto;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsbyCategory(int catID)
        {
            var posts = await _postData.GetAllPostbyCategories(catID);
            var postsDto = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return postsDto;
        }

        public async Task<IEnumerable<PostDTO>> GetAllPostsbySearch(string query)
        {
            var posts = await _postData.GetAllPostbySearch(query);
            var postsDto = _mapper.Map<IEnumerable<PostDTO>>(posts);
            return postsDto;
        }

        public async Task<IEnumerable<DislikePostDTO>> GetDislikePost(int postID)
        {
            var post = await _postData.GetDislike(postID);
            var postDto = _mapper.Map<IEnumerable<DislikePostDTO>>(post);
            return postDto;
        }

        public async Task<IEnumerable<LikePostDTO>> GetLikePost(int postID)
        {
            var post = await _postData.GetLike(postID);
            var postDto = _mapper.Map<IEnumerable<LikePostDTO>>(post);
            return postDto;
        }

        public async Task<PostDTO> GetPostbyID(int postID)
        {
            var post = await _postData.GetPostbyID(postID);
            var postDto = _mapper.Map<PostDTO>(post);
            return postDto;
        }

        public async Task LikePost(int postID, int userID)
        {
            try
            {
                await _postData.LikePost(postID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UndislikePost(int postID, int userID)
        {
            try
            {
                await _postData.UndislikePost(postID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UnlikePost(int postID, int userID)
        {
            try
            {
                await _postData.UnlikePost(postID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
