using FluentValidation;
using GreenShopFinal.Service.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.Validations.Auth
{
    public class RegisterDtoValidation:AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidation()
        {
            RuleFor(a => a.UserName)
                .NotEmpty()
                .NotNull();
            RuleFor(a => a.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(8);
            RuleFor(a => a.Email)
                .EmailAddress()
                .NotNull()
                .NotEmpty();
            RuleFor(a => a)
                .Custom((x, context) =>
                {
                    if(x.Password!=x.ConfirmPassword)
                    {
                        context.AddFailure((x.ConfirmPassword), "Password doesn't match,please try again!");
                    }
                });
        }
    }
}
