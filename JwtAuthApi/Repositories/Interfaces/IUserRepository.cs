using JwtAuthApi.Models;

namespace JwtAuthApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetByUsernameAndPassword(string username, string password);
        User? GetUserById(string userId);
    }
}
