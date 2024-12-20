﻿using AutoMapper;
using GreenShopFinal.Core.Entities;
using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Data.Context;
using GreenShopFinal.Service.ApiResponses;
using GreenShopFinal.Service.DTOs.Product;
using GreenShopFinal.Service.Services.AbstractServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GreenShopFinal.Service.Services.ConcreteServices
{
    public class ProductServices : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;
        private readonly GreenShopFinalDbContext _dbContext;
        private readonly UserManager<BaseUser> _userManager;
        public ProductServices(IProductRepository productRepository, IMapper mapper, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IPhotoService photoService, GreenShopFinalDbContext dbContext, UserManager<BaseUser> userManager)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<ApiResponse> Create(ProductPostDto dto)
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                CategoryId = dto.CategoryId,
                ShortDescription = dto.ShortDescription,
                LongDescription = dto.LongDescription,
                Name = dto.Name,
                Price = dto.Price,
                Size = dto.ProductSize,
                UserId = dto.UserId
            };

            string root = _env.WebRootPath;
            string path = "assets/img/product";
            var req = _httpContextAccessor.HttpContext.Request;
            var ismain = 0;
            foreach (var image in dto.Images)
            {
                var res = await _photoService.AddPhotoAsync(image);
                BaseImage baseImage = new BaseImage()
                {
                    Id = Guid.NewGuid(),
                    Product = product,
                    CreatedDate = DateTime.UtcNow,
                    IsMain = ismain == 0 ? true : false,
                    Image = res.DisplayName
                };
                _dbContext.BaseImages.Add(baseImage);
            }
            ismain++;
            await _productRepository.AddAsync(product);
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 201 };
        }

        public async Task<ApiResponse> GetAll()
        {
            var products = _productRepository.GetAll(x => !x.IsDeleted);
            List<ProductGetDto> dtos = new List<ProductGetDto>();
            dtos = await products.Select(p => new ProductGetDto
            {
                Name = p.Name,
                Id = p.Id,
                Price = p.Price,
                ShortDescription = p.ShortDescription,
                LongDescription = p.LongDescription,
                Images = p.Images,
                ProductSize = p.Size,
                Category = p.Category
            }).ToListAsync();
            return new ApiResponse { StatusCode = 200, Data = dtos };
        }

        public async Task<ApiResponse> GetById(Guid id)
        {
            var product = await _productRepository.GetAsync(p => p.IsDeleted && p.Id == id);
            if (product == null)
                return new ApiResponse { StatusCode = 404, Message = "item is not found" };
            ProductGetDto productGetDto = _mapper.Map<ProductGetDto>(product);
            return new ApiResponse { StatusCode = 200, Data = productGetDto };
        }

        public async Task<ApiResponse> Remove(Guid id)
        {
            var product = await _productRepository.GetAsync(p => !p.IsDeleted && p.Id == id);
            if (product == null)
                return new ApiResponse { StatusCode = 404, Message = "item is not found" };
            product.IsDeleted = true;
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204 };
        }
        public async Task<ApiResponse> Update(Guid id, ProductPutDto dto)
        {
            var product = await _productRepository.GetAsync(p => !p.IsDeleted && p.Id == id);
            if (product == null)
                return new ApiResponse { StatusCode = 404, Message = "Item is not found" };
            product.CategoryId = dto.CategoryId;
            product.Name = dto.Name;
            product.Price = dto.Price;
            product.ShortDescription = dto.ShortDescription;
            product.LongDescription = dto.LongDescription;
            //product.Image = dto.File == null ? product.Image : await dto.File.SaveFileAsync(_env.WebRootPath, "assets/img/product");
            product.UpdatedDate = DateTime.Now;
            await _productRepository.SaveAsync();
            return new ApiResponse { StatusCode = 204, Message = "Item is updated" };
        }

    }
}
