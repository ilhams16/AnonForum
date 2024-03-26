using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.Comment;
using AnonForum.API.BLL.DTOs.Post;
using AnonForum.API.BLL.Interfaces;
using AnonForum.API.Data;
using AnonForum.API.Data.Interfaces;
using AnonForum.API.Domain;
using AutoMapper;

namespace AnonForum.API.BLL
{
    public class CommentBLL : ICommentBLL
    {
        private readonly ICommentData _commentData;
        private readonly IMapper _mapper;

        public CommentBLL(ICommentData commentData, IMapper mapper)
        {
            _commentData = commentData;
            _mapper = mapper;
        }

        public async Task<CommentDTO> AddNewComment(CreateCommentDTO entity)
        {
            try
            {
                var comment = _mapper.Map<Comment>(entity);
                var result = await _commentData.AddNewComment(comment);
                var commentDto = _mapper.Map<CommentDTO>(result);
                return commentDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CommentDTO> EditComment(EditCommentDTO entity)
        {
            try
            {
                var comment = _mapper.Map<Comment>(entity);
                await _commentData.EditComment(entity.CommentID, comment);
                var commentDto = _mapper.Map<CommentDTO>(comment);
                return commentDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteComment(int commentID)
        {
            try
            {
                await _commentData.DeleteComment(commentID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DislikeComment(int commentID, int userID, int postID)
        {
            try
            {
                await _commentData.DislikeComment(commentID, userID, postID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<IEnumerable<CommentDTO>> GetAllCommentbyPostID(int postID)
        {
            var comments = await _commentData.GetAllCommentbyPostID(postID);
            var commentsDto = _mapper.Map<IEnumerable<CommentDTO>>(comments);
            return commentsDto;
        }

        public async Task<CommentDTO> GetCommentbyUserIDandPostID(int userID, int postID)
        {
            var comments = await _commentData.GetCommentbyUserIDandPostID(userID, postID);
            var commentsDto = _mapper.Map<CommentDTO>(comments);
            return commentsDto;
        }

        public async Task<CommentDTO> GetCommentbyCommentID(int commentID)
        {
            var comments = await _commentData.GetCommentbyCommentID(commentID);
            var commentsDto = _mapper.Map<CommentDTO>(comments);
            return commentsDto;
        }

        public async Task<IEnumerable<DislikeCommentDTO>> GetDislike(int commentID)
        {
            var comment = await _commentData.GetDislike(commentID);
            var commentDto = _mapper.Map<IEnumerable<DislikeCommentDTO>>(comment);
            return commentDto;
        }

        public async Task<IEnumerable<LikeCommentDTO>> GetLike(int commentID)
        {
            var comment = await _commentData.GetLike(commentID);
            var commentDto = _mapper.Map<IEnumerable<LikeCommentDTO>>(comment);
            return commentDto;
        }

        public async Task LikeComment(int commentID, int userID, int postID)
        {
            try
            {
                await _commentData.LikeComment(commentID, userID, postID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UndislikeComment(int commentID, int userID)
        {
            try
            {
                await _commentData.UndislikeComment(commentID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UnlikeComment(int commentID, int userID)
        {
            try
            {
                await _commentData.UnlikeComment(commentID, userID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
