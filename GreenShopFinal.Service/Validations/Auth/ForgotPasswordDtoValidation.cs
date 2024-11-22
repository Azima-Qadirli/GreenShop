using FluentValidation;
using GreenShopFinal.Service.DTOs.Auth;

namespace GreenShopFinal.Service.Validations.Auth
{
    public class ForgotPasswordDtoValidation : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordDtoValidation()
        {
            RuleFor(fp => fp.NewPassword)
                .NotEmpty()
                .NotNull();
            RuleFor(fp => fp.ConfirmPassword)
                .NotEmpty()
                .NotNull();
            RuleFor(fp => fp)
                .Custom((fp, context) =>
                {
                    if (fp.ConfirmPassword != fp.NewPassword)
                    {
                        context.AddFailure(nameof(fp.ConfirmPassword), "Passwords don't match.");
                    }
                });
        }
    }
}
