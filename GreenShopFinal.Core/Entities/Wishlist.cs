using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenShopFinal.Core.Entities
{
    public class Wishlist : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public BaseUser User { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}
