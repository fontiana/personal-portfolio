using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace PersonalPortfolio.Helper
{
    public class ImageHelper : IImageHelper
    {
        private readonly string uploadPath = "uploads";
        private IWebHostEnvironment hostingEnvironment;

        public ImageHelper(IWebHostEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string getFilePath(string filename)
        {
            return Path.Combine("/", uploadPath, filename);
        }

        public async Task<string> saveFileAsync(IFormFile formFile)
        {
            string uploadsPath = Path.Combine(hostingEnvironment.WebRootPath, uploadPath);

            if (!Directory.Exists(uploadsPath))
            {
                Directory.CreateDirectory(uploadsPath);
            }

            string filePath = Path.Combine(uploadsPath, formFile.FileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            return formFile.FileName;
        }
    }
}
