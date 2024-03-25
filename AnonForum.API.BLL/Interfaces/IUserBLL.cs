using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnonForum.API.BLL.DTOs.User;

namespace AnonForum.API.BLL.Interfaces
{
    public interface IUserBLL
    {
        Task<UserDTO> AddNewUser(CreateUserDTO entity);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task<UserDTO> EditNickname(string username, string Nickname);
        Task DeleteUser(string username);
        Task<UserDTO> GetUserbyID(int id);
        Task<UserDTO> GetUserbyUsername(string username);
        Task<UserDTO> UserLogin(UserLoginDTO entity);
        Task<IEnumerable<UserDTO>> GetUsersWithRoles();
        Task<UserDTO> GetUserbyIDWithRoles(int id);
        Task ChangePassword(string username, string newPassword);
    }
}
