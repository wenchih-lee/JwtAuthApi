using JwtAuthApi.Models;
using JwtAuthApi.Services.Interfaces;

namespace JwtAuthApi.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new()
        {
            new User { Id = "1", Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Id = "2", Username = "user", Password = "user123", Role = "User" }
        };

        public User? ValidateUser(string username, string password)
        {
            return _users.FirstOrDefault(u =>
                u.Username == username && u.Password == password);
        }
    }
}
