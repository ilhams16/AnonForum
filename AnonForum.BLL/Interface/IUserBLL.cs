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
        List<UserAuth> GetUsers();
        UserAuth UserLogin(string usernameOrEmail, string password);
        string EditNickname(string username, string Nickname);
        string DeleteUser(string username);
        IEnumerable<UserDTO> GetWithPaging(int pageNumber, int pageSize, string name);
        UserDTO GetUserbyID(int id);
    }
}
