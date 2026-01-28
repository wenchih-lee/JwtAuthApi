namespace JwtAuthApi.Models
{
    public class UserProfileResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string UserRole {  get; set; } = string.Empty;
    }
}
