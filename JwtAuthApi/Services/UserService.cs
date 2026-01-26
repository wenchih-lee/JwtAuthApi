using JwtAuthApi.DTOs;
using JwtAuthApi.Repositories.Interfaces;
using JwtAuthApi.Services.Interfaces;

namespace JwtAuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto? ValidateUser(string username, string password)
        {
            var user = _userRepository.GetByUsernameAndPassword(username, password);

            if (user == null)
                return null;

            // 將 Entity 轉換為 DTO (不包含密碼)
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
