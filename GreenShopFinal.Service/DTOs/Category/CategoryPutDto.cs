using Microsoft.AspNetCore.Http;

namespace GreenShopFinal.Service.DTOs
{
    public record CategoryPutDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public IFormFile? File { get; set; }
    }
}
