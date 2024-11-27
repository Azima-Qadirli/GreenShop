using FluentValidation;
using FluentValidation.AspNetCore;
using GreenShopFinal.Extensions;
using GreenShopFinal.Register;
using GreenShopFinal.Service.Helpers;
using GreenShopFinal.Service.Mapping;
using GreenShopFinal.Service.Validations.Category;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<CategoryPostDtoValidation>().AddFluentValidationClientsideAdapters();

builder.Services.AddAutoMapper(typeof(CategoryMap));
builder.Services.AddAutoMapper(typeof(ProductMap));
builder.Services.AddAutoMapper(typeof(WishListMap));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.
    RegisterService(builder.Configuration)
    .RegisterJWTService(builder.Configuration)
    .RegisterUserServices()
    .RegisterConfigureService();

//CloudinarySettings
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureException();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors("api");
app.Run();
