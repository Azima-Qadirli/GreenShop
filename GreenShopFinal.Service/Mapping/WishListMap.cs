using AutoMapper;
using GreenShopFinal.Core.Entities;
using GreenShopFinal.Service.DTOs.WishList;

namespace GreenShopFinal.Service.Mapping
{
    public class WishListMap : Profile
    {
        public WishListMap()
        {
            CreateMap<WishListPostDto, Wishlist>().ReverseMap();
            CreateMap<Wishlist, WishListGetDto>().ReverseMap();
        }
    }
}
