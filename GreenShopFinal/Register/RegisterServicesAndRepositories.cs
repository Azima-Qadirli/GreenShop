using GreenShopFinal.Core.Repositories.Abstractions;
using GreenShopFinal.Data.Context;
using GreenShopFinal.Data.Repositories.Concretes;
using GreenShopFinal.Service.Services.AbstractServices;
using GreenShopFinal.Service.Services.ConcreteServices;
using Microsoft.EntityFrameworkCore;

namespace GreenShopFinal.Register
{
    public static class RegisterServicesAndRepositories
    {
        public static IServiceCollection RegisterService(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<GreenShopFinalDbContext>(opt =>
             {
                 opt.UseSqlServer(config.GetConnectionString("Default"));
             });

            services.AddCors(opt =>
            {
                opt.AddPolicy("api", option =>
                {
                    option.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            //repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();

            //services
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IWishListService, WishListService>();
            services.AddTransient<ITokenHandler, TokenHandler>();
            services.AddScoped<IProductService, ProductServices>();
            services.AddTransient<IGoogleIdTokenValidationService, GoogleIdTokenValidationService>();
            services.AddScoped<IAdminLoginService, AdminLoginService>();
            return services;
        }
    }
}
