namespace JwtAuthApi.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        string GenerateRefreshToken();
        void SaveRefreshToken(string userId, string refreshToken);
        bool ValidateRefreshToken(string refreshToken, out string userId);
        void RevokeRefreshToken(string refreshtoken);
    }
}
