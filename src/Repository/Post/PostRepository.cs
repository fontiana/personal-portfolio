using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Context;

namespace PersonalPortfolio.Repository.Post
{
    public class PostRepository : IPostRepository
    {
        private readonly PortfolioContext context;

        public PostRepository(PortfolioContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Context.Post entity)
        {
            await context.Posts.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            Context.Post post = await context.Posts.FindAsync(id);
            context.Posts.Remove(post);
        }

        public async Task<List<Context.Post>> GetAsync()
        {
            return await context.Posts.ToListAsync();
        }

        public async Task<Context.Post> GetByIDAsync(int id)
        {
            return await context.Posts.FindAsync(id);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Update(Context.Post entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
