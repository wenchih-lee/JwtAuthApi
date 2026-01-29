using JwtAuthApi.Models;

namespace JwtAuthApi.Services.Interfaces
{
    public interface ILoginLogService
    {
        private static readonly List<LoginLog> _logs = new();

        void LogLogin(string username, bool isSuccess, string clientIp);
        List<LoginLog> GetLogs();
    }
}
