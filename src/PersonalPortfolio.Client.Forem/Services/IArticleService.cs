using System.Collections.Generic;
using System.Threading.Tasks;
using PersonalPortfolio.Client.Forem.Models;

namespace PersonalPortfolio.Client.Forem.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetArticlesByUserAsync(string username, int page = 1, int perPage = 30);
    }
}
