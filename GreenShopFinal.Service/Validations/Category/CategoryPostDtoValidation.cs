using FluentValidation;
using GreenShopFinal.Service.DTOs;
using GreenShopFinal.Service.Extensions;
using Microsoft.AspNetCore.Http;

namespace GreenShopFinal.Service.Validations.Category
{
    public class CategoryPostDtoValidation : AbstractValidator<CategoryPostDto>
    {
        public CategoryPostDtoValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(c => c.File).Custom((file, context) =>
            {
                if (!file.IsImage())
                {
                    context.AddFailure((nameof(IFormFile)), "File doesn't contain image");
                }
                if (!file.IsSizeOk(5))
                {
                    context.AddFailure((nameof(IFormFile)), "File's size must be maximum 5mb");
                }
            });
        }
    }
}
