using Microsoft.AspNetCore.Identity;

namespace GreenShopFinal.Core.Entities.AppUser
{
    public class BaseUser : IdentityUser
    {
        public List<Product> Products { get; set; }
        public List<Wishlist> Wishlists { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
