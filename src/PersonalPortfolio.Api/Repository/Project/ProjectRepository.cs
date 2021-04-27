﻿using System.Collections.Generic;
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

        public async Task AddAsync(ProjectEntity entity)
        {
            await context.Projects.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var project = await context.Projects.FindAsync(id);
            context.Projects.Remove(project);
        }

        public async Task<List<ProjectEntity>> GetAsync()
        {
            return await context.Projects.ToListAsync();
        }

        public async Task<ProjectEntity> GetByIdAsync(int id)
        {
            return await context.Projects
                .Include(r => r.Technologies)
                .FirstOrDefaultAsync(r => r.ProjectId == id);
        }

        public async Task<ProjectEntity> GetByTitleAsync(string title)
        {
            return await context.Projects
                .Include(r => r.Technologies)
                .FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Update(ProjectEntity entity)
        {
            context.Projects.Update(entity);
        }
    }
}