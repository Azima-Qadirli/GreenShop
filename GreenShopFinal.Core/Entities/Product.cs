using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Core.Entities.Base;
using GreenShopFinal.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenShopFinal.Core.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public double Price { get; set; }
        public List<BaseImage> Images { get; set; }
        public ProductSize Size { get; set; }

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public BaseUser User { get; set; }
    }
}
