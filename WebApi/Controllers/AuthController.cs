
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };

            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded ? Ok("User created") : BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

            return result.Succeeded ? Ok("Login success") : BadRequest("Invalid login");
        }
    }
}
