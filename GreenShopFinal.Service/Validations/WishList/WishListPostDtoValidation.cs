using FluentValidation;
using GreenShopFinal.Service.DTOs.WishList;

namespace GreenShopFinal.Service.Validations.WishList
{
    public class WishListPostDtoValidation : AbstractValidator<WishListPostDto>
    {
        public WishListPostDtoValidation()
        {
            RuleFor(w => w.UserId)
                .NotEmpty()
                .NotNull();
            RuleFor(w => w.ProductId)
                .NotEmpty()
                .NotNull();
        }
    }
}
