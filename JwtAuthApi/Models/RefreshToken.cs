using System.Diagnostics.Eventing.Reader;

namespace JwtAuthApi.Models
{
    public class RefreshToken
    {
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
    }
}
