using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace GreenShopFinal.Service.Services.AbstractServices
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);
        Task<DeletionResult> DeletePhotoAsync(Guid id);
    }
}
