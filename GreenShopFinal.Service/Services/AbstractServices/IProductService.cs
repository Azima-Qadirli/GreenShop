using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
