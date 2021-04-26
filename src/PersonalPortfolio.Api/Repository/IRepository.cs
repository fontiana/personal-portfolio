using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonalPortfolio.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(int id);
        void Update(TEntity entity);
        Task Save();
    }
}
