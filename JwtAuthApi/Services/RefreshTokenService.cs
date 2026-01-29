using JwtAuthApi.Models;
using JwtAuthApi.Services.Interfaces;
using System.Diagnostics.Eventing.Reader;

namespace JwtAuthApi.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        // 代替資料庫
        private static readonly List<RefreshToken> _refreshTokens = new();

        public string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }

        public void SaveRefreshToken(string userId, string refreshToken)
        {
            _refreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(1),
                IsRevoked = false
            });
        }

        public bool ValidateRefreshToken(string refreshToken, out string userId)
        {
            userId = string.Empty;

            var token = _refreshTokens.FirstOrDefault(x => x.Token == refreshToken && !x.IsRevoked && x.ExpiresAt > DateTime.UtcNow);

            if (token == null) return false;

            userId = token.UserId;
            return true;
        }

        public void RevokeRefreshToken(string refreshToken)
        {
            var token = _refreshTokens.FirstOrDefault(t => t.Token == refreshToken);
            if (token != null)
            {
                token.IsRevoked = true;
            }
        }
    }
}
