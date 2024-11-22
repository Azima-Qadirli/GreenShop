﻿using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Auth;
using GreenShopFinal.Service.Exceptions;
using GreenShopFinal.Service.GoogleToken;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class AuthService : IAuthService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BaseUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<BaseUser> _signManager;
        private readonly IMailService _mailService;
        private static readonly Dictionary<string, (int Code, DateTime Expiry)> verificationCodes = new Dictionary<string, (int Code, DateTime Expiry)>
        {

        };
        private static readonly Dictionary<string, (int Code, DateTime Expiry)> resetCodes = new Dictionary<string, (int Code, DateTime Expiry)>
        {

        };
        private readonly IGoogleIdTokenValidationService _googleIdTokenValidationService;
        public AuthService(RoleManager<IdentityRole> roleManager, UserManager<BaseUser> userManager, IConfiguration configuration, SignInManager<BaseUser> signManager, IMailService mailService, IGoogleIdTokenValidationService googleIdTokenValidationService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
            _signManager = signManager;
            _mailService = mailService;
            _googleIdTokenValidationService = googleIdTokenValidationService;
        }
        [HttpPost("login")]
        public async Task<ApiResponse> Login(LoginDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.UserName);
            if (userExists == null && await _userManager.CheckPasswordAsync(userExists, dto.Password))
                return new ApiResponse { StatusCode = 404, Message = "User not found" };

            var userRoles = await _userManager.GetRolesAsync(userExists);
            var claim = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,dto.UserName),
                new Claim(ClaimTypes.NameIdentifier,userExists.Id)
            };
            foreach (var role in userRoles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }
            var secret_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:secret_key"]));
            var jwtToken = new JwtSecurityToken
                (
                    issuer: _configuration["JWT:issuer"],
                    audience: _configuration["JWT:audience"],
                    expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration["JWT:datetime"])),
                    claims: claim,
                    signingCredentials: new SigningCredentials(secret_key, SecurityAlgorithms.HmacSha256)
                );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return new ApiResponse { StatusCode = 200, Data = token };
        }
        public async void Logout()
        {
            await _signManager.SignOutAsync();
        }
        public async Task<ApiResponse> Register(RegisterDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.UserName);
            if (userExists != null)
                return new ApiResponse { StatusCode = 302, Message = "user is already exists" };
            var user = new BaseUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var res = await _userManager.CreateAsync(user);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    return new ApiResponse { Data = error.Description };
                }
            }
            try
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            var result = await SendEmail(user);
            return new ApiResponse { StatusCode = 200, Data = result.Data };
        }
        public async Task<ApiResponse> SendEmail(BaseUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            Random random = new Random();
            int code = random.Next(1000, 9999);
            verificationCodes[user.Id] = (code, DateTime.Now.AddMinutes(5));
            _mailService.SendMail(user.Email, "Verify your email", $"Please verify your email to finish registration process {code}");
            return new ApiResponse { StatusCode = 200, Data = (user, token) };
        }
        public async Task<ApiResponse> ConfirmEmail(string userId, string token, int input)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new UserNotFoundException("User not found");

            if (verificationCodes.TryGetValue(user.Id, out var verification))
            {
                if (verification.Expiry < DateTime.Now || verification.Code != input)
                {
                    return new ApiResponse { StatusCode = 404, Message = "Verification code is not valid" };
                }
                verificationCodes.Remove(user.Id);
                user.EmailConfirmed = true;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    return new ApiResponse { StatusCode = 500, Message = "Failed to update user confirmation status" };
                }
                await _userManager.ConfirmEmailAsync(user, token);
                await _signManager.SignInAsync(user, isPersistent: true);
                return new ApiResponse { StatusCode = 200, Message = "Confirmation is successful." };
            }
            return new ApiResponse { StatusCode = 404 };
        }
        public async Task<Token> GoogleLogin(GoogleLoginDto dto)
        {
            var result = await _googleIdTokenValidationService.ValidateIdTokenAsync(dto);
            return result;
        }
        public ApiResponse SendResetPasswordEmail(string email)
        {

            Random random = new Random();
            int code = random.Next(1000, 9999);
            resetCodes[email] = (code, DateTime.Now.AddMinutes(2));
            _mailService.SendMail(email, "Password reset email", $"Please use this {code} for resetting your password.");
            return new ApiResponse { StatusCode = 200, Data = new { Email = email } };
        }
        public ApiResponse ConfirmPassword(string email, int code)
        {
            if (resetCodes.TryGetValue(email, out var reset))
            {
                if (reset.Expiry < DateTime.Now || reset.Code != code)
                {
                    return new ApiResponse { StatusCode = 400, Message = "Code is not valid" };
                }
                resetCodes.Remove(email);
                return new ApiResponse { StatusCode = 200, Data = new { Email = email } };
            }
            return new ApiResponse { StatusCode = 404, Message = "No matching account." };
        }
        public async Task<ApiResponse> ForgotPassword(string email, ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                //return new ApiResponse { StatusCode = 404 };
                throw new UserNotFoundException("User is not found.");
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var res = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);

            if (res == null)
                return new ApiResponse { StatusCode = 500 };

            return new ApiResponse { StatusCode = 200, Message = "Password reset successfully" };
        }


    }
}
