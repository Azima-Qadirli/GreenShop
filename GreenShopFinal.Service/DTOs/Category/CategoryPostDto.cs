using GreenShopFinal.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace GreenShopFinal.Service.DTOs
{
    public record CategoryPostDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public IFormFile File { get; set; }
        public ProductSize ProductSize { get; set; }
    }
}
