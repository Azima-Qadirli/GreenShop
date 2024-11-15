using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Auth;
using GreenShopFinal.Service.GoogleToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface IGoogleIdTokenValidationService
    {
        public Task<Token> ValidateIdTokenAsync(GoogleLoginDto dto);
    }
}
