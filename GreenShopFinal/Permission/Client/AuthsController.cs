using GreenShopFinal.Service.DTOs.Auth;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GreenShopFinal.Permission.Client
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IGoogleIdTokenValidationService _googleIdTokenValidationService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AuthsController(IAuthService authService, IGoogleIdTokenValidationService googleIdTokenValidationService, RoleManager<IdentityRole> roleManager)
        {
            _authService = authService;
            _googleIdTokenValidationService = googleIdTokenValidationService;
            _roleManager = roleManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto dto)
        {
            var res = await _authService.Register(dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto dto)
        {
            var res = await _authService.Login(dto);
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token, int code)
        {
            var res = await _authService.ConfirmEmail(userId, token, code);
            return StatusCode(res.StatusCode, res.Message);
        }
        //[HttpPost("google-login")]
        //public async Task<IActionResult> GoogleLogin([FromForm]GoogleLoginDto dto)
        //{
        //    var res = await _authService.GoogleLogin(dto);
        //    return new Token { dto.IdToken };
        //}
        [HttpPost("googleLogin")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto dto)
        {
            var token = await _googleIdTokenValidationService.ValidateIdTokenAsync(dto);
            return Ok(token);
        }
        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromQuery] string email, ForgotPasswordDto dto)
        {
            var res = await _authService.ForgotPassword(email, dto);
            return StatusCode(res.StatusCode, res.Message);
        }



        //[HttpPost("CreateRole")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole admin = new() { Name = "Admin" };
        //    IdentityRole superAdmin = new() { Name = "superAdmin" };
        //    IdentityRole user = new() { Name = "User" };

        //    await _roleManager.CreateAsync(admin);
        //    await _roleManager.CreateAsync(superAdmin);
        //    await _roleManager.CreateAsync(user);
        //    return Ok();
        //}
    }
}
