using Microsoft.AspNetCore.Mvc;
using TradeManagement.Models;
using TradeManagement.Repositories;
using TradeManagement.Services;
using System.Security.Cryptography;
using System.Text;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepo, JwtService jwtService)
        {
            _userRepo = userRepo;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User request)
        {
            if (await _userRepo.GetUserByUsernameAsync(request.Username) != null)
                return BadRequest("Username already exists.");

            // Hash password
            using var hmac = new HMACSHA256();
            request.PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.PasswordHash)));

            await _userRepo.AddUserAsync(request);

            return Ok(new { Message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User request)
        {
            var user = await _userRepo.GetUserByUsernameAsync(request.Username);
            if (user == null)
                return Unauthorized("Invalid username.");

            using var hmac = new HMACSHA256();
            var hashed = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(request.PasswordHash)));
            if (user.PasswordHash != hashed)
                return Unauthorized("Invalid password.");

            var token = _jwtService.GenerateJwtToken(user.Id, user.Username, user.Roles.ToArray());
            return Ok(new { Token = token });
        }
    }
}
