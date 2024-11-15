using GreenShopFinal.Service.ApiResponses;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface IWishListService
    {
        public Task<ApiResponse> AddProductToWishlist(string userId, Guid productId);
        public Task<ApiResponse> RemoveProductFromWishlist(string userId, Guid productId);
        public Task<ApiResponse> GetWishlist(string userId, Guid productId);
    }
}
