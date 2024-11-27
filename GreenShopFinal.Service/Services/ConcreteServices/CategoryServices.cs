using AutoMapper;
using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs;
using GreenShopFinal.Service.Exceptions;
using GreenShopFinal.Service.Extensions;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class CategoryServices : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<BaseUser> _userManager;
        public CategoryServices(ICategoryRepository categoryRepository, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, UserManager<BaseUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<ApiResponse> Create(CategoryPostDto dto)
        {
            Category category = _mapper.Map<Category>(dto);
            category.Image = await dto.File.SaveFileAsync(_env.WebRootPath, "assets/img/category");
            var req = _httpContextAccessor.HttpContext.Request;
            category.ImageUrl = req.Scheme + "://" + req.Host + "assets/img/category";
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveAsync();
            await dto.File.SaveFileAsync(_env.WebRootPath, "assets/img/category");
            return new ApiResponse { StatusCode = 201 };
        }
        public async Task<ApiResponse> GetAll()
        {
            var categories = _categoryRepository.GetAll(x => !x.IsDeleted);
            List<CategoryGetDto> dtos = new List<CategoryGetDto>();
            dtos = await categories.Select(c => new CategoryGetDto { Name = c.Name, Id = c.Id, Image = c.Image, ImageUrl = c.ImageUrl }).ToListAsync();
            return new ApiResponse { StatusCode = 200, Data = dtos };
        }
        public async Task<ApiResponse> GetById(Guid id)
        {
            var category = await _categoryRepository.GetAsync(x => x.Id == id);
            if (category is null || category.IsDeleted)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
            //throw new CategoryNotFound("Category Not Found");
            CategoryGetDto dto = _mapper.Map<CategoryGetDto>(category);
            return new ApiResponse { StatusCode = 200, Data = dto };
        }
        public async Task<ApiResponse> Remove(Guid id)
        {
            var category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (category is null)
                //return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
                throw new ItemNotFoundException("Item not found");
            category.IsDeleted = true;
            await _categoryRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
        public async Task<ApiResponse> Update(Guid id, CategoryPutDto dto)
        {
            var category = await _categoryRepository.GetAsync(x => !x.IsDeleted && x.Id == id);
            if (category is null)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
            category.Name = dto.Name;
            //category.Image = dto.File == null ? category.Image : await dto.File.SaveFileAsync("/assets/img/category/", _env.WebRootPath);
            category.UpdatedDate = DateTime.UtcNow;
            await _categoryRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204, Message = "Item is updated" };

        }
    }
}
