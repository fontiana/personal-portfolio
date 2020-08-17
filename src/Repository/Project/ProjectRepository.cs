using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Context;

namespace PersonalPortfolio.Repository.Project
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly PortfolioContext context;

        public ProjectRepository(PortfolioContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Context.Project entity)
        {
            await context.Projects.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            Context.Project project = await context.Projects.FindAsync(id);
            context.Projects.Remove(project);
        }

        public async Task<List<Context.Project>> GetAsync()
        {
            return await context.Projects.ToListAsync();
        }

        public async Task<Context.Project> GetByIDAsync(int id)
        {
            return await context.Projects.FindAsync(id);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Context.Project entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
