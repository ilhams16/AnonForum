using System;
using System.Collections.Generic;
using System.Text;
using AnonForum.BLL.DTOs.Post;
using AnonForum.BLL.DTOs.User;
using AnonForum.BO;

namespace AnonForum.BLL.Interface
{
    public interface IUserBLL
    {
        string AddNewUser(CreateUserDTO entity);
        IEnumerable<UserDTO> GetUsers();
        string EditNickname(string username, string Nickname);
        string DeleteUser(string username);
        IEnumerable<UserDTO> GetWithPaging(int pageNumber, int pageSize, string name);
        UserDTO GetUserbyID(int id);
        UserDTO GetUserbyUsername(string username);
        UserDTO UserLogin(UserLoginDTO entity);
    }
}
