using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Context;
using PersonalPortfolio.Context.Entity;

namespace PersonalPortfolio.Repository.Post
{
    public class PostRepository : IPostRepository
    {
        private readonly PortfolioContext context;

        public PostRepository(PortfolioContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(PostEntity entity)
        {
            await context.Posts.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var post = await context.Posts.FindAsync(id);
            context.Posts.Remove(post);
        }

        public async Task<List<PostEntity>> GetAsync()
        {
            return await context.Posts
                .Include(r => r.Category)
                .ToListAsync();
        }

        public async Task<PostEntity> GetByIdAsync(int id)
        {
            return await context.Posts
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.PostId == id);
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
        }

        public void Update(PostEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
