using AnonForum.API.Data.Helpers;
using AnonForum.API.Data.Interfaces;
using AnonForum.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace AnonForum.API.Data
{
    public class UserData : IUserData
    {
        private readonly AppDbContext _context;
        public UserData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Task> ChangePassword(string username, string newPassword)
        {
            try
            {
                var user = await _context.UserAuths.FirstOrDefaultAsync(u => u.Username == username);
                if (user == null)
                {
                    throw new ArgumentException("User not found");
                }
                user.Password = Helpers.Md5Hash.GetHash(newPassword);
                await _context.SaveChangesAsync();
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }
        public async Task<UserAuth> AddNewUser(UserAuth user)
        {
            try
            {
                _context.UserAuths.FromSqlRaw("EXEC [dbo].[NewUser] {0}, {1}, {2}, {3}, {4}", user.Username, user.Email, user.Password, user.Nickname, user.UserImage).AsEnumerable();
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message} : {ex.InnerException.Message}");
            }
        }

        public Task AddUserCommunity(int commID, int userID)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(string username)
        {
            var user = await _context.UserAuths.FindAsync(username);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public Task EditNickname(string username, string Nickname)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserAuth>> GetAllUser()
        {
            var users = await _context.UserAuths.ToListAsync();
            return users;
        }
        public async Task<IEnumerable<UserAuth>> GetAllUserWithRoles()
        {
            var users = await _context.UserAuths.Include(u=>u.Roles).ToListAsync();
            return users;
        }

        public async Task<UserAuth> GetbyID(int id)
        {
            var user = await _context.UserAuths.FindAsync(id);
            return user;
        }

        public async Task<UserAuth> GetbyIDWithRoles(int id)
        {
            var user = await _context.UserAuths.Include(u=>u.Roles).FirstOrDefaultAsync(u=> u.UserId == id);
            return user;
        }

        public async Task<UserAuth> GetbyUsername(string username)
        {
            var user = await _context.UserAuths.FindAsync(username);
            return user;
        }

        public async Task<UserAuth> UserLogin(string usernameOrEmail, string password)
        {
            var user = _context.UserAuths.FromSqlRaw("EXEC UserLogin {0}, {1}", usernameOrEmail, password).AsEnumerable().FirstOrDefault();
            //var user = await _context.UserAuths.FirstOrDefaultAsync(u=>u.Username == usernameOrEmail && u.Password == Helpers.Md5Hash.GetHash(password));
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }
            return user;
        }
    }
}
