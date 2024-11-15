using FluentValidation;
using GreenShopFinal.Service.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.Validations.Auth
{
    public class LoginDtoValidation:AbstractValidator<LoginDto>
    {
        public LoginDtoValidation()
        {
            RuleFor(l => l.UserName)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.Password)
                .NotEmpty()
                .NotNull();
        }
    }
}
