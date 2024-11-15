using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Mvc;

namespace GreenShopFinal.Permission.Client.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("category/getall")]
        public async Task<IActionResult> GetAll()
        {
            var res = await _productService.GetAll();
            return StatusCode(res.StatusCode, res.Data);
        }
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var res = await _productService.GetById(id);
            return StatusCode(res.StatusCode, res.Data);
        }
    }
}
