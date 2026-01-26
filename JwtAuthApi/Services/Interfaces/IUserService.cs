using JwtAuthApi.DTOs;

namespace JwtAuthApi.Services.Interfaces
{
    public interface IUserService
    {
        UserDto? ValidateUser(string username, string password);
    }
}
