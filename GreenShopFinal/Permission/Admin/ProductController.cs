using GreenShopFinal.Service.DTOs.Product;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenShopFinal.Permission.Admin.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductPostDto dto)
        {
            var res = await _productService.Create(dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpPut("category/{id}")]
        public async Task<IActionResult> Update(Guid id, ProductPutDto dto)
        {
            var res = await _productService.Update(id, dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var res = await _productService.Remove(id);
            return StatusCode(res.StatusCode);
        }

    }
}
