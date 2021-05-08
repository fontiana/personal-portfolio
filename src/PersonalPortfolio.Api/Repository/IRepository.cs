using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalPortfolio.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAsync(CancellationToken cancellationToken);
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        void Update(TEntity entity);
        Task Save(CancellationToken cancellationToken);
    }
}
