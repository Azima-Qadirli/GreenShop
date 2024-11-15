using Microsoft.AspNetCore.Identity;

namespace GreenShopFinal.Register
{
    public static class RegisterConfigure
    {
        public static IServiceCollection RegisterConfigureService(this IServiceCollection services)
        {
            services.Configure<IdentityOptions>(opt =>
            {
                //password
                opt.Password.RequiredUniqueChars = 1;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;

                //lockout
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.AllowedForNewUsers = true;

                //signin
                opt.SignIn.RequireConfirmedEmail = false;
                opt.SignIn.RequireConfirmedAccount = false;
                opt.SignIn.RequireConfirmedPhoneNumber = false;

                //user
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
            });
            return services;
        }
    }
}
