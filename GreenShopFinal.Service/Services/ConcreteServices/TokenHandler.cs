using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class TokenHandler : ITokenHandler
    {
        private readonly string? _audience;
        private readonly IConfiguration _configuration;
        private readonly string? _issuer;
        private readonly byte[] _key;
        private readonly UserManager<BaseUser> _userManager;

        public TokenHandler(IConfiguration configuration, UserManager<BaseUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _issuer = _configuration["JWT:issuer"]!;
            _audience = _configuration["JWT:audience"]!;
            _key = Encoding.UTF8.GetBytes(_configuration["JWT:secret_key"]!);

            if (_issuer is null || _audience is null)
                throw new ArgumentNullException();
        }

        public async Task<string> CreateAccessTokenWithRoleAsync(BaseUser user, string role, DateTime expire)
        {
            if (!await _userManager.IsInRoleAsync(user, role))
                throw new Exception();

            var tokenDesc = CreateTokenDescriptor(user, expire);
            tokenDesc.Subject.AddClaim(new Claim(ClaimTypes.Role, role));

            var handler = new JwtSecurityTokenHandler();
            var accesToken = handler.CreateToken(tokenDesc);

            return handler.WriteToken(accesToken);
        }

        public async Task<string> CreateAccessTokenAsync(BaseUser user, DateTime expireDate)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var tokenDesc = CreateTokenDescriptor(user, expireDate);

            foreach (var role in roles) tokenDesc.Subject.AddClaim(new Claim(ClaimTypes.Role, role));

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.CreateToken(tokenDesc);

            return handler.WriteToken(accessToken);
        }

        public string CreateRefreshToken()
        {
            var nums = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(nums);
            return Convert.ToBase64String(nums);
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(BaseUser user, DateTime expireDate)
        {
            var tokenDesc = new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                Expires = expireDate,
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(_key), SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(new List<Claim>
        {
            new("Id", user.Id),
           // new("Picture", user.ProfilePhoto ?? _configuration["Defaults:DefaultUser"]!),
            new(JwtRegisteredClaimNames.Sub, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        })
            };

            //tokenDesc.Subject.AddClaim(new Claim("IsVerified", user.IsVerified.ToString()));
            tokenDesc.Subject.AddClaim(new Claim("HasSuggestions", false.ToString()));

            //if (user is not Human h) return tokenDesc;

            //tokenDesc.Subject.AddClaim(new Claim("FirstName", h.FirstName));
            //tokenDesc.Subject.AddClaim(new Claim("LastName", h.LastName));
            //tokenDesc.Subject.AddClaim(new Claim("MiddleName", h.MiddleName ?? string.Empty));
            //tokenDesc.Subject.AddClaim(new Claim("Balance", h.Balance.ToString("C")));

            return tokenDesc;
        }
    }

}
