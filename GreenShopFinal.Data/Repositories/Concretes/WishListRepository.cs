using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Data.Context;

namespace GreenShopFinal.Data.Repositories.Concretes
{
    public class WishListRepository : Repository<Wishlist>, IWishListRepository
    {
        public WishListRepository(GreenShopFinalDbContext greenShopFinalDbContext) : base(greenShopFinalDbContext)
        {
        }
    }
}
