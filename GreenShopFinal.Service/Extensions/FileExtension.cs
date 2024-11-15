using Microsoft.AspNetCore.Http;

namespace GreenShopFinal.Service.Extensions
{
    public static class FileExtension
    {
        public static bool IsImage(this IFormFile file)
        {
            return file.ContentType.Contains("image");
        }
        public static bool IsSizeOk(this IFormFile file, int mb)
        {
            return file.Length / 1024 / 1024 <= mb;
        }
        public static async Task<string> SaveFileAsync(this IFormFile file, string root, string path)
        {
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string fullPath = Path.Combine(root, path, fileName);
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        public static void RemoveFile(this IFormFile file, string root, string path, string fileName)
        {
            var fullPath = Path.Combine(root, path, fileName);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}
