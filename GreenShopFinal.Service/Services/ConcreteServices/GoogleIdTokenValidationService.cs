using Google.Apis.Auth;
using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Service.DTOs.Auth;
using GreenShopFinal.Service.Exceptions;
using GreenShopFinal.Service.GoogleToken;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using static Google.Apis.Auth.GoogleJsonWebSignature;

namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class GoogleIdTokenValidationService : IGoogleIdTokenValidationService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<BaseUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        public GoogleIdTokenValidationService(IConfiguration configuration, UserManager<BaseUser> userManager, ITokenHandler tokenHandler)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> ValidateIdTokenAsync(GoogleLoginDto dto)
        {
            ValidationSettings? settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string>()
                {
                    _configuration["Google:Client_ID"]
                }
            };
            Payload payload = await GoogleJsonWebSignature.ValidateAsync(dto.IdToken, settings);
            UserLoginInfo userLoginInfo = new(dto.Provider, payload.Subject, dto.Provider);
            BaseUser user = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email
                    };
                    IdentityResult createResult = await _userManager.CreateAsync(user);
                    result = createResult.Succeeded;
                }
            }
            if (result)
                await _userManager.AddLoginAsync(user, userLoginInfo);
            else
                throw new InvalidExternalAuthentication("Invalid external authentication.");
            var expiredDate = DateTime.UtcNow.AddDays(1);
            var token = await _tokenHandler.CreateAccessTokenAsync(user, expiredDate);

            return new Token();
        }

    }
}
