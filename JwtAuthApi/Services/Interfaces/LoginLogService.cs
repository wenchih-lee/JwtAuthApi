using JwtAuthApi.Models;

namespace JwtAuthApi.Services.Interfaces
{
    public class LoginLogService : ILoginLogService
    {
        private static readonly List<LoginLog> _logs = new();

        public void LogLogin(string username, bool isSuccess, string clientIp)
        {
            _logs.Add(new LoginLog
            {
                Username = username,
                IsSuccess = isSuccess,
                ClientIp = clientIp,
                LoginTime = DateTime.UtcNow
            });
        }

        public List<LoginLog> GetLogs()
        {
            return _logs;
        }
    }
}
