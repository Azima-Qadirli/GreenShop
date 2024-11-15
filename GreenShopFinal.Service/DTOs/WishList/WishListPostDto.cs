using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.DTOs.WishList
{
    public record WishListPostDto
    {
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
