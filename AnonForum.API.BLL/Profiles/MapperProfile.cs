using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.Comment;
using AnonForum.API.BLL.DTOs.Post;
using AnonForum.API.BLL.DTOs.User;
using AnonForum.API.Domain;
using AutoMapper;

namespace AnonForum.API.BLL.Profiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<UserAuth, UserDTO>().ReverseMap();
            CreateMap<CreateUserDTO, UserAuth>();

            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<CreatePostDTO, Post>();
            CreateMap<EditPostDTO, Post>();

            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<CreateCommentDTO, Comment>();
            CreateMap<EditCommentDTO, Comment>();

            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<UnlikePost, DislikePostDTO>().ReverseMap();
            CreateMap<LikePost, LikePostDTO>().ReverseMap();

            CreateMap<UnlikeComment, DislikeCommentDTO>().ReverseMap();
            CreateMap<LikeComment, LikeCommentDTO>().ReverseMap();

            CreateMap<PostCategory, PostCategoryDTO>().ReverseMap();
        }
    }
}
