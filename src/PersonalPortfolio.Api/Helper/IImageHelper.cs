using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PersonalPortfolio.Helper
{
    public interface IImageHelper
    {
        Task<string> saveFileAsync(IFormFile formFile);
        string getFilePath(string filename);
    }
}
