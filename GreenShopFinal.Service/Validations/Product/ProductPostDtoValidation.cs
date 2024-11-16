using FluentValidation;
using GreenShopFinal.Service.DTOs.Product;

namespace GreenShopFinal.Service.Validations.Product
{
    public class ProductPostDtoValidation : AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidation()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
            RuleFor(p => p.ShortDescription)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
            RuleFor(p => p.LongDescription)
                .NotEmpty()
                .NotNull()
                .MaximumLength(300);
            //RuleFor(p => p.Images).Custom((file, context) =>
            //{
            //    if (!file.IsImage())
            //    {
            //        context.AddFailure(nameof(IFormFile),"File doesn't contain an image");
            //    }
            //    if (!file.IsSizeOk(5))
            //    {
            //        context.AddFailure(nameof(IFormFile), "File's can be maximum 5mb.");
            //    }
            //});
        }
    }
}
