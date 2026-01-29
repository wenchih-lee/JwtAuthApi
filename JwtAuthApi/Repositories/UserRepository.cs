using JwtAuthApi.Models;
using JwtAuthApi.Repositories.Interfaces;

namespace JwtAuthApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        // 模擬資料庫資料
        private static readonly List<User> _users = new List<User>()
        {
            new User { Id = "1", Username = "Vince", Password = "vince123", Role = "Admin" },
            new User { Id = "2", Username = "Jimmy", Password = "jimmy456", Role = "User" },
            new User { Id = "3", Username = "Ian", Password = "ian789", Role = "User" },
        };

        public User? GetByUsernameAndPassword(string username, string password)
        {
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }

        public User? GetUserById(string userId)
        {
            return _users.FirstOrDefault(u => u.Id == userId);
        }
    }
}
