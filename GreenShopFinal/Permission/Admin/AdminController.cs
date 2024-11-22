using GreenShopFinal.Service.DTOs.Auth;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace GreenShopFinal.Permission.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminLoginService _adminLoginService;

        public AdminController(IAdminLoginService adminLoginService)
        {
            _adminLoginService = adminLoginService;
        }
        [HttpPost("admin-login")]
        public async Task<IActionResult> AdminLogin(AdminLoginDto dto)
        {
            //if (_adminLoginService.ValidateAdmin(dto.UserName, dto.Password))
            //{
            //    return Ok(new { Message = "Admin logged in successfully" });
            //}
            //return Unauthorized(new { Message = "Invalid admin credentials" });
            var res = await _adminLoginService.AdminLogin(dto);
            return StatusCode(res.StatusCode, res.Data);

        }
        [HttpPost("confirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string userId, string token, int code)
        {
            var res = await _adminLoginService.ConfirmEmail(userId, token, code);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpPost("admin-register")]
        public async Task<IActionResult> AdminRegister(AdminRegisterDto dto)
        {
            var res = await _adminLoginService.AdminRegister(dto);
            return StatusCode(res.StatusCode, res.Data);
        }
    }
}
