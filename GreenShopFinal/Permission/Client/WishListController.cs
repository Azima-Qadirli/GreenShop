using GreenShopFinal.Service.Extensions;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenShopFinal.Permission.Client
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IWishListService _wishListService;

        public WishListController(IWishListService wishListService)
        {
            _wishListService = wishListService;
        }
        [HttpPost("{productId}/add")]
        public async Task<IActionResult> AddProductToWishlist(Guid productId)
        {
            var userId = User.GetUserId();
            var res = await _wishListService.AddProductToWishlist(userId, productId);
            return StatusCode(res.StatusCode);
        }
        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveProductFromWishList(string userId, Guid productId)
        {
            var res = await _wishListService.RemoveProductFromWishlist(userId, productId);
            return StatusCode(res.StatusCode);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWishList(string userId, Guid productId)
        {
            var res = await _wishListService.GetWishlist(userId, productId);
            return StatusCode(res.StatusCode);
        }
    }
}
