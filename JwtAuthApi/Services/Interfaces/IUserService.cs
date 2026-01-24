using JwtAuthApi.Models;

namespace JwtAuthApi.Services.Interfaces
{
    public interface IUserService
    {
        User? ValidateUser(string username, string password);
    }
}
