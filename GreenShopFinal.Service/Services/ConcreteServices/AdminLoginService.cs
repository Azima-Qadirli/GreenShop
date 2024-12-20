﻿using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Auth;
using GreenShopFinal.Service.Exceptions;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class AdminLoginService : IAdminLoginService
    {
        private readonly UserManager<BaseUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly SignInManager<BaseUser> _signManager;
        private static readonly Dictionary<string, (int Code, DateTime Expiry)> verificationCodes = new Dictionary<string, (int Code, DateTime Expiry)>
        {

        };

        public AdminLoginService(UserManager<BaseUser> userManager, IConfiguration configuration, IMailService mailService, SignInManager<BaseUser> signManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _signManager = signManager;
        }

        public async Task<ApiResponse> AdminRegister(AdminRegisterDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.UserName);
            if (userExists != null)
                return new ApiResponse { StatusCode = 302, Message = "user already exists" };
            var user = new BaseUser()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var res = await _userManager.CreateAsync(user, dto.Password);
            if (!res.Succeeded)
            {
                foreach (var error in res.Errors)
                {
                    return new ApiResponse { Data = error.Description };
                }
            }
            try
            {
                await _userManager.AddToRoleAsync(user, "Admin");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            var result = await SendMail(user);
            return new ApiResponse { StatusCode = 200, Data = result.Data };
        }

        public async Task<ApiResponse> AdminLogin(AdminLoginDto dto)
        {
            var userExists = await _userManager.FindByNameAsync(dto.UserName);
            //if (userExists != null && await _userManager.CheckPasswordAsync(userExists, dto.Password))
            //    //return new ApiResponse { StatusCode = 404,Message="User not found"};
            //    throw new UserNotFoundException("User not found");
            if (userExists == null)
                throw new UserNotFoundException("User not found ");
            if (!await _userManager.CheckPasswordAsync(userExists, dto.Password))
                throw new UserNotFoundException("User not found");
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
            await _userManager.UpdateAsync(userExists);
            return new ApiResponse { StatusCode = 200, Data = token };
            //return username == _userName && password == _password;
        }
        public async Task<ApiResponse> SendMail(BaseUser user)
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
                    verificationCodes.Remove(user.Id);
                    return new ApiResponse { StatusCode = 404, Message = "Verification code is not valid" };
                }
                //verificationCodes.Remove(user.Id);
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

    }
}
