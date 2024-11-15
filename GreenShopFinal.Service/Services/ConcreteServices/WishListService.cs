using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Data.Context;
using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.WishList;
using GreenShopFinal.Service.Exceptions;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class WishListService : IWishListService
    {
        private readonly GreenShopFinalDbContext _greenShopFinalDbContext;
        private readonly UserManager<BaseUser> _userManager;
        private readonly IProductRepository _productRepository;
        public WishListService(GreenShopFinalDbContext greenShopFinalDbContext, UserManager<BaseUser> userManager, IProductRepository productRepository)
        {
            _greenShopFinalDbContext = greenShopFinalDbContext;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task<ApiResponse> AddProductToWishlist(string userId, Guid productId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new UserNotFoundException("User not found");

            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                throw new ProductNotFoundException("Product not found");

            var wishlist = new Wishlist()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                ProductId = productId,
                UserId = user.Id,
            };
            try
            {
                await _greenShopFinalDbContext.AddAsync(wishlist);
                await _greenShopFinalDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return new ApiResponse { StatusCode = 201, Data = wishlist.Id };
        }

        public async Task<ApiResponse> GetWishlist(string userId, Guid productId)
        {
            var products = _productRepository.GetAll(p => p.IsDeleted);
            List<WishListGetDto> dtos = new List<WishListGetDto>();
            dtos = await products.Select(p => new WishListGetDto { UserId = p.UserId, ProductId = p.Id }).ToListAsync();
            return new ApiResponse { StatusCode = 200, Data = dtos };
        }

        public async Task<ApiResponse> RemoveProductFromWishlist(string userId, Guid productId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new UserNotFoundException("User not found");

            var wishList = await _greenShopFinalDbContext.Wishlists.FirstOrDefaultAsync(w => w.UserId == user.Id);

            if (wishList == null)
                return new ApiResponse { StatusCode = 404, Message = "WishList is not found for a specified user." };
            wishList.IsDeleted = true;
            await _greenShopFinalDbContext.SaveChangesAsync();
            return new ApiResponse { StatusCode = 204 };
        }
    }
}
