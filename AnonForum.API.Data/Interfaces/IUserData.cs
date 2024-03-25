using AnonForum.API.Domain;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AnonForum.API.Data.Interfaces
{
    public interface IUserData
    {
        public Task AddUserCommunity(int commID, int userID);
        public Task<UserAuth> AddNewUser(UserAuth user);
        public Task<IEnumerable<UserAuth>> GetAllUser();
        public Task<UserAuth> UserLogin(string usernameOrEmail, string password);
        public Task EditNickname(string username, string Nickname);
        public Task DeleteUser(string username);
        public Task<UserAuth> GetbyUsername(string username);
        public Task<UserAuth> GetbyID(int id);
        Task<UserAuth> GetbyIDWithRoles(int id);
        Task<IEnumerable<UserAuth>> GetAllUserWithRoles();
        Task<Task> ChangePassword(string username, string newPassword);
    }
}
