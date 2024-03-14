using System;
using System.Collections.Generic;
using AnonForum.BLL.DTOs.User;
using AnonForum.BLL.Interface;
using AnonForum.BO;
using AnonForum.DAL;
using static Dapper.SqlMapper;

namespace AnonForum.BLL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUser _userDAL;
        public UserBLL()
        {
            _userDAL = new UserDAL();
        }

        public string AddNewUser(CreateUserDTO entity)
        {
            if (string.IsNullOrEmpty(entity.Username))
            {
                throw new ArgumentException("Username is required");
            }
            else if (entity.Username.Length > 50)
            {
                throw new ArgumentException("Username max length is 50");
            }

            try
            {
                var newUser = new UserAuth
                {
                    Username = entity.Username,
                    Email = entity.Email,
                    Password = entity.Password,
                    Nickname = entity.Nickname,
                    UserImage = entity.UserImage,
                };
                return (string)_userDAL.AddNewUser(newUser);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public string DeleteUser(string username)
        {
            if (username.Length <= 0)
            {
                throw new ArgumentException("UserID is required");
            }

            try
            {
                return (string)_userDAL.DeleteUser(username);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }

        public string EditNickname(string username, string Nickname)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            List<UserDTO> listUsersDto = new List<UserDTO>();
            var users = _userDAL.GetAllUser();
            foreach (var user in users)
            {
                listUsersDto.Add(new UserDTO
                {
                    UserID = user.UserID,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password,
                    Nickname = user.Nickname,
                    UserImage = user.UserImage
                });
            }
            return listUsersDto;
        }

        public IEnumerable<UserDTO> GetWithPaging(int pageNumber, int pageSize, string name)
        {
            throw new NotImplementedException();
        }

        public UserDTO UserLogin(UserLoginDTO entity)
        {
            UserDTO userDto = new UserDTO();
            var user = _userDAL.UserLogin(entity.UsernameOrEmail, entity.Password);
            if (user != null)
            {
                userDto.UserID = user.UserID;
                userDto.Username = user.Username;
                userDto.Email = user.Email;
                userDto.Password = user.Password;
                userDto.Nickname = user.Nickname;
                userDto.UserImage = user.UserImage;
            }
            else
            {
                throw new ArgumentException($"{entity.UsernameOrEmail} not found");
            }
            return userDto;
        }
        public UserDTO GetUserbyUsername(string username)
        {
            UserDTO userDto = new UserDTO();
            var user = _userDAL.GetbyUsername(username);
            if (user != null)
            {
                userDto.UserID = user.UserID;
                userDto.Username = user.Username;
                userDto.Email = user.Email;
                userDto.Password = user.Password;
                userDto.Nickname = user.Nickname;
                userDto.UserImage = user.UserImage;
            }
            else
            {
                throw new ArgumentException($"{username} not found");
            }
            return userDto;
        }
        public UserDTO GetUserbyID(int id)
        {
            UserDTO userDto = new UserDTO();
            var user = _userDAL.GetbyID(id);
            if (user != null)
            {
                userDto.UserID = user.UserID;
                userDto.Username = user.Username;
                userDto.Email = user.Email;
                userDto.Password = user.Password;
                userDto.Nickname = user.Nickname;
                userDto.UserImage = user.UserImage;
            }
            else
            {
                throw new ArgumentException($"User id {id} not found");
            }
            return userDto;
        }

        public void AddUserCommunity(AddUserCommunityDTO userCommunityDTO) 
        {
            _userDAL.AddUserCommunity(userCommunityDTO.CommunityID, userCommunityDTO.UserID);
        }

    }
}
