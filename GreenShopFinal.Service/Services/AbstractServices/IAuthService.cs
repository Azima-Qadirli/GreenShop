using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Auth;
using GreenShopFinal.Service.GoogleToken;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface IAuthService
    {
        public Task<ApiResponse> Login(LoginDto dto);
        public Task<Token> GoogleLogin(GoogleLoginDto dto);
        public Task<ApiResponse> Register(RegisterDto dto);
        public Task<ApiResponse> ConfirmEmail(string userId, string token, int input);
        public Task<ApiResponse> SendEmail(BaseUser user);
        public Task<ApiResponse> ForgotPassword(string email, ForgotPasswordDto dto);
        void Logout();

    }
}
