using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PersonalPortfolio.Helper
{
    public static class ImageHelper
    {
        public static async Task SaveImageAsync(this IFormFile target, CancellationToken cancellationToken)
        {
            var filePath = Path.GetTempFileName();
            using (var stream = File.Create(filePath))
            {
                await target.CopyToAsync(stream, cancellationToken);
            }
        }

        public static string LoadImage(this string fileName)
        {
            var filePath = Path.GetTempFileName();
            return Path.Combine(filePath, fileName);
        }
    }
}
