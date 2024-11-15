using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface ICategoryService
    {
        public Task<ApiResponse> Create(CategoryPostDto dto);
        public Task<ApiResponse> GetAll();
        public Task<ApiResponse> GetById(Guid id);
        public Task<ApiResponse> Remove(Guid id);
        public Task<ApiResponse> Update(Guid id,CategoryPutDto dto);
        
    }
}
