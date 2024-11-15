using GreenShopFinal.Core.Entities.AppUser;
using GreenShopFinal.Data.Context;
using Microsoft.AspNetCore.Identity;

namespace GreenShopFinal.Register
{
    public static class RegisterUser
    {
        public static IServiceCollection RegisterUserServices(this IServiceCollection services)
        {
            //identity
            services.AddIdentity<BaseUser, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = true;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireNonAlphanumeric = false;
                 options.Password.RequireUppercase = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequiredUniqueChars = 1;

                 // Default Lockout settings.
                 options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                 options.Lockout.MaxFailedAccessAttempts = 5;
                 options.Lockout.AllowedForNewUsers = true;

                 // Default SignIn settings.
                 options.SignIn.RequireConfirmedEmail = false;
                 options.SignIn.RequireConfirmedPhoneNumber = false;

                 // Default User settings.
                 options.User.AllowedUserNameCharacters =
                         "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                 options.User.RequireUniqueEmail = false;
             })
                 .AddEntityFrameworkStores<GreenShopFinalDbContext>()
                 .AddDefaultTokenProviders();

            return services;
        }

    }
}
