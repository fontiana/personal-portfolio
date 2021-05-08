using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Context.Entity;

namespace PersonalPortfolio.Repository.Post
{
    public interface IPostRepository : IRepository<PostEntity>
    {
        Task<PostEntity> GetByTitleAsync(string title, CancellationToken cancellationToken);
        Task<List<PostEntity>> GetByCategoryAsync(string categoryTitle, CancellationToken cancellationToken);
    }
}
