
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Models;
using WebApi.Models.Auth;
using WebApi.Models.Common;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded ? Ok("User created") : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
                return Unauthorized(ApiResponse<string>.Fail("Invalid user"));

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return Unauthorized(ApiResponse<string>.Fail("User not found"));

            // Lấy roles
            var roles = await _userManager.GetRolesAsync(user);

            // Nếu bạn có bảng Permissions, bạn load thêm ở đây
            var permissions = new List<string>();

            var profile = new UserProfileResponse
            {
                Email = user.Email!,
                FullName = user.UserName,
                AvatarUrl = "",
                Roles = roles.ToList(),
                Permissions = permissions
            };

            return Ok(ApiResponse<UserProfileResponse>.Ok(profile, "Authenticated"));
        }


        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(ApiResponse<string>.Ok(email!, "Authenticated"));
        }
    }
}
