using JwtAuthApi.Helpers;
using JwtAuthApi.Models;
using JwtAuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly JwtHelpers _jwtHelper;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ILoginLogService _loginLogService;

        public AuthController(IUserService userService, JwtHelpers jwtHelper, IRefreshTokenService refreshTokenService, ILoginLogService loginLogService)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
            _refreshTokenService = refreshTokenService;
            _loginLogService = loginLogService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var clientIp = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var user = _userService.ValidateUser(request.Username, request.Password);

            if (user == null)
            {
                _loginLogService.LogLogin(request.Username, false, clientIp);
                return Unauthorized(new { message = "帳號或密碼錯誤" });
            }

            _loginLogService.LogLogin(request.Username, true, clientIp);

            var accesstoken = _jwtHelper.GenerateToken(user.Id, user.Username, user.Role);
            var refreshToken = _refreshTokenService.GenerateRefreshToken();

            _refreshTokenService.SaveRefreshToken(user.Id, refreshToken);

            return Ok(new TokenResponse 
            { 
                AccessToken = accesstoken,
                RefreshToken = refreshToken
            });
        }

        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshTokenRequest request)
        {
            if (!_refreshTokenService.ValidateRefreshToken(request.RefreshToken, out var userId))
            {
                return Unauthorized(new { message = "無效的 Refresh Token" });
            }

            var user = _userService.GetUserById(userId);

            if (user == null)
            {
                return Unauthorized(new { message = "使用者不存在" });
            }

            var newAccessToken = _jwtHelper.GenerateToken(user.Id, user.Username, user.Role);
            var newRefreshToken = _refreshTokenService.GenerateRefreshToken();

            _refreshTokenService.RevokeRefreshToken(request.RefreshToken);
            _refreshTokenService.SaveRefreshToken(user.Id, newRefreshToken);

            return Ok(new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpGet("logs")]
        public IActionResult GetLogs()
        {
            return Ok(_loginLogService.GetLogs());
        }
    }
}
