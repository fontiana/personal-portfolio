using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PersonalPortfolio.Helper
{
    public static class ImageHelper
    {
        public static async Task SaveImageAsync(this IFormFile target, CancellationToken cancellationToken = default(CancellationToken))
        {
            var filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await target.CopyToAsync(stream);
            }
        }

        public static void LoadImage(this string fileName)
        {
            var filePath = Path.GetTempFileName();
            Path.Combine(filePath, fileName);
        }
    }
}
