using GreenShopFinal.Service.Exceptions;
using System.Security.Claims;

namespace GreenShopFinal.Service.Extensions
{
    public static class ClaimExtension
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            if (user.Claims == null)
                throw new ArgumentNullException(nameof(user.Claims));

            var claim = user.Identities.First().Claims.FirstOrDefault(c => c.Type == "Id");

            if (claim == null)
                throw new TokenInvalidException("Token is not found");

            return claim.Value;
        }
    }
}
