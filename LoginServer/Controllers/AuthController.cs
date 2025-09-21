using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LoginServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // 회원가입 DTO
        public record RegisterDto([Required] string Email, [Required] string Password);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new IdentityUser { UserName = registerDto.Email, Email = registerDto.Email };
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return Ok(new { Message = "회원가입 성공" });
            }

            return BadRequest(result.Errors);
        }

        // 로그인 DTO
        public record LoginDto([Required] string Email, [Required] string Password);

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Ok(new { Message = "로그인 성공" });
            }
            
            if (result.IsLockedOut)
            {
                return Unauthorized(new { Message = "계정이 잠겼습니다." });
            }

            return Unauthorized(new { Message = "로그인 실패: 이메일 또는 비밀번호를 확인하세요." });
        }
    }
}