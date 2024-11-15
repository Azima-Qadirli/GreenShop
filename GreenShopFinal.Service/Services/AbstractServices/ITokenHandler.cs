using GreenShopFinal.Core.Entities.AppUser;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface ITokenHandler
    {
        Task<string> CreateAccessTokenAsync(BaseUser user, DateTime expireDate);
        Task<string> CreateAccessTokenWithRoleAsync(BaseUser user, string role, DateTime expire);
        string CreateRefreshToken();
    }

}
