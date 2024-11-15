using AutoMapper;
using GreenShopFinal.Core.Entities;
using GreenShopFinal.Service.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenShopFinal.Service.Mapping
{
    public class ProductMap:Profile
    {
        public ProductMap()
        {
            CreateMap<ProductPostDto, Product>().ReverseMap();
            CreateMap<ProductPutDto, Product>().ReverseMap();
            CreateMap<Product,ProductGetDto>().ReverseMap();
        }
    }
}
