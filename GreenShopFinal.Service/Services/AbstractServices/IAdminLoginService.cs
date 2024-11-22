using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Auth;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface IAdminLoginService
    {
        public Task<ApiResponse> AdminRegister(AdminRegisterDto dto);
        public Task<ApiResponse> SendMail(BaseUser user);
        public Task<ApiResponse> ConfirmEmail(string userId, string token, int input);
        public Task<ApiResponse> AdminLogin(AdminLoginDto dto);
    }
}
