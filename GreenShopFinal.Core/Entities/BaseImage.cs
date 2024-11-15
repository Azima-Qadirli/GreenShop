using GreenShopFinal.Core.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace GreenShopFinal.Core.Entities
{
    public class BaseImage : BaseEntity
    {
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsMain { get; set; }
        public string Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
