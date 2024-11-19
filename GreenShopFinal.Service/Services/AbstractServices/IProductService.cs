using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Product;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface IProductService
    {
        public Task<ApiResponse> Create(ProductPostDto dto);
        public Task<ApiResponse> GetAll();
        public Task<ApiResponse> GetById(Guid id);
        public Task<ApiResponse> Remove(Guid id);
        public Task<ApiResponse> Update(Guid id, ProductPutDto dto);
    }
}
