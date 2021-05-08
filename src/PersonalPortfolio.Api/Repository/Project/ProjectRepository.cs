using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Context;
using PersonalPortfolio.Context.Entity;

namespace PersonalPortfolio.Repository.Project
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PortfolioContext context;

        public ProjectRepository(PortfolioContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(ProjectEntity entity, CancellationToken cancellationToken)
        {
            await context.Projects.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var project = await context.Projects.FindAsync(id, cancellationToken);
            context.Projects.Remove(project);
        }

        public async Task<List<ProjectEntity>> GetAsync(CancellationToken cancellationToken)
        {
            return await context.Projects.ToListAsync(cancellationToken);
        }

        public async Task<ProjectEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Projects
                .Include(r => r.Technologies)
                .FirstOrDefaultAsync(r => r.ProjectId == id, cancellationToken);
        }

        public async Task<ProjectEntity> GetByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await context.Projects
                .Include(r => r.Technologies)
                .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public void Update(ProjectEntity entity)
        {
            context.Projects.Update(entity);
        }
    }
}
