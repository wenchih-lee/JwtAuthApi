using JwtAuthApi.Models;

namespace JwtAuthApi.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private static readonly List<RefreshToken> _refreshTokens = new();

        public string GenerateToken()
        {
            return Guid.NewGuid().ToString();
        }

        public void SaveRefreshToken(string userId, string refreshToken)
        {

        }
    }
}
