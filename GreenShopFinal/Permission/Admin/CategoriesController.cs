using GreenShopFinal.Service.DTOs;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GreenShopFinal.Permission.Admin.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,SuperAdmin")]
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CategoryPostDto dto)
        {
            var res = await _categoryService.Create(dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpPut("category/{id}")]
        public async Task<IActionResult> Update(Guid id, CategoryPutDto dto)
        {
            var res = await _categoryService.Update(id, dto);
            return StatusCode(res.StatusCode, res.Message);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(Guid id)
        {
            var res = await _categoryService.Remove(id);
            return StatusCode(res.StatusCode);
        }

    }
}
