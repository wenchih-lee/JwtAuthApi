namespace JwtAuthApi.Models
{
    public class LoingLog
    {
        public string Username { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string ClientIp { get; set; } = string.Empty;
        public DateTime LoginTime { get; set; }
    }
}
