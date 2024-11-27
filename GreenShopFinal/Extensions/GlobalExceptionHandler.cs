using GreenShopFinal.Service.Exceptions.BaseExceptionHandler;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace GreenShopFinal.Extensions
{
    public static class GlobalExceptionHandler
    {
        public static void ConfigureException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var statusCode = (int)HttpStatusCode.InternalServerError;
                    var message = "Internal Server Error";
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is BaseException ex)
                        {
                            message = ex.Message;
                            statusCode = (int)ex.StatusCode;
                        }
                        context.Response.StatusCode = statusCode;
                        var result = JsonSerializer.Serialize(new { statusCode = statusCode, message = message });
                        await context.Response.WriteAsync(result);
                    }
                });
            });
        }
    }
}
