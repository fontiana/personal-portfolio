using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Client.Forem.Models;

namespace PersonalPortfolio.Client.Forem.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> GetArticlesByUserAsync(CancellationToken cancellationToken);
    }
}
