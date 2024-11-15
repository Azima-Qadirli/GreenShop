using GreenShopFinal.Core.Entities.Base;

namespace GreenShopFinal.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
        public List<Product> Products { get; set; }
    }
}
