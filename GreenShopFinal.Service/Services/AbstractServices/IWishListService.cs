using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.WishList;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface IWishListService
    {
        public Task<ApiResponse> AddProductToWishlist(WishListPostDto dto);
        public Task<ApiResponse> RemoveProductFromWishlist(string userId, Guid productId);
        public Task<ApiResponse> GetWishlist(string userId, Guid productId);
    }
}
