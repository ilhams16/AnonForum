using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.User;
using AnonForum.API.BLL.Interfaces;
using AnonForum.API.Data.Interfaces;
using AnonForum.API.Domain;
using AutoMapper;

namespace AnonForum.API.BLL
{
    public class UserBLL : IUserBLL
    {
        private readonly IUserData _userData;
        private readonly IMapper _mapper;

        public UserBLL(IUserData userData, IMapper mapper)
        {
            _userData = userData;
            _mapper = mapper;
        }

        public Task ChangePassword(string username, string newPassword)
        {
            var user = _userData.ChangePassword(username, newPassword);
            return user;
        }

        public async Task<UserDTO> AddNewUser(CreateUserDTO entity)
        {
            try
            {
                var user = _mapper.Map<UserAuth>(entity);
                var result = await _userData.AddNewUser(user);
                var userDto = _mapper.Map<UserDTO>(result);
                return userDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUser(string username)
        {
            try
            {
                await _userData.DeleteUser(username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<UserDTO> EditNickname(string username, string Nickname)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDTO> GetUserbyID(int id)
        {
            var user = await _userData.GetbyID(id);
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<UserDTO> GetUserbyIDWithRoles(int id)
        {
            var user = await _userData.GetbyIDWithRoles(id);
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<UserDTO> GetUserbyUsername(string username)
        {
            var user = await _userData.GetbyUsername(username);
            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var users = await _userData.GetAllUser();
            var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            return usersDto;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersWithRoles()
        {
            var users = await _userData.GetAllUserWithRoles();
            var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            return usersDto;
        }

        public async Task<UserDTO> UserLogin(UserLoginDTO entity)
        {
            var user = await _userData.UserLogin(entity.UsernameOrEmail, entity.Password);
            if (user == null)
            {
                throw new ArgumentException("Invalid username or password");
            }
            var userWithRoles = await _userData.GetbyIDWithRoles(user.UserId);
            var userDTO = _mapper.Map<UserDTO>(userWithRoles);
            return userDTO;
        }
    }
}
