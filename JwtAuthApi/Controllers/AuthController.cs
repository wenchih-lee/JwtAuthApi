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

        public AuthController(IUserService userService, JwtHelpers jwtHelper)
        {
            _userService = userService;
            _jwtHelper = jwtHelper;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _userService.ValidateUser(request.Username, request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "帳號或密碼錯誤" });
            }

            var token = _jwtHelper.GenerateToken(user.Id, user.Username, user.Role);

            return Ok(new LoginResponse { Token = token });
        }
    }
}
