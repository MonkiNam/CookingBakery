using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Repositories.Utils
{
    public static class UploadImage
    {
        public static async Task<string> UploadFile(IFormFile file, IWebHostEnvironment environment)
        {
            var baseDirectory = Path.Combine(environment.WebRootPath, "image");
            if (!Directory.Exists(baseDirectory))
            {
                Directory.CreateDirectory(baseDirectory);
            }
            var filePath = Path.Combine(baseDirectory, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return file.FileName;
        }
    }
}
