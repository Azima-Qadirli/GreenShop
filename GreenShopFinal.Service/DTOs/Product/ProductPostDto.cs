﻿using GreenShopFinal.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace GreenShopFinal.Service.DTOs.Product
{
    public record ProductPostDto
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public IFormFileCollection Images { get; set; }
        public Guid CategoryId { get; set; }
        public ProductSize ProductSize { get; set; }
    }
}
