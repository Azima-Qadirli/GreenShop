using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Enums;

namespace GreenShopFinal.Service.DTOs.Product
{
    public record ProductGetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<BaseImage> Images { get; set; }
        public ProductSize ProductSize { get; set; }
        public Category Category { get; set; }
        public List<Review>? Reviews { get; set; }

    }
}
