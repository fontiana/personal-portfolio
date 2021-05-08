using System.Threading;
using System.Threading.Tasks;
using PersonalPortfolio.Context.Entity;

namespace PersonalPortfolio.Repository.Project
{
    public interface IProjectRepository : IRepository<ProjectEntity>
    {
        Task<ProjectEntity> GetByTitleAsync(string title, CancellationToken cancellationToken);
    }
}
