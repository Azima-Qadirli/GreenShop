using AutoMapper;
using GreenShopFinal.Core.Entities;
using GreenShopFinal.Service.DTOs;

namespace GreenShopFinal.Service.Mapping
{
    public class CategoryMap:Profile
    {
        public CategoryMap()
        {
            CreateMap<CategoryPostDto,Category>().ReverseMap();
            CreateMap<CategoryPutDto,Category>().ReverseMap();
            CreateMap<Category,CategoryGetDto>().ReverseMap();
        }
    }
}
