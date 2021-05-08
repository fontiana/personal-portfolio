using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        public async Task AddAsync(PostEntity entity, CancellationToken cancellationToken)
        {
            await context.Posts.AddAsync(entity, cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var post = await context.Posts.FindAsync(id, cancellationToken);
            context.Posts.Remove(post);
        }

        public async Task<List<PostEntity>> GetAsync(CancellationToken cancellationToken)
        {
            return await context.Posts
                .Include(r => r.Category)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<PostEntity>> GetByCategoryAsync(string categoryTitle, CancellationToken cancellationToken)
        {
            return await context.Posts
                .Where(x => x.Category.Name == categoryTitle)
                .Include(r => r.Category)
                .ToListAsync(cancellationToken);
        }

        public async Task<PostEntity> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await context.Posts
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.PostId == id, cancellationToken);
        }

        public async Task<PostEntity> GetByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await context.Posts
                .Include(r => r.Category)
                .FirstOrDefaultAsync(x => x.Title == title, cancellationToken);
        }

        public async Task Save(CancellationToken cancellationToken)
        {
            await context.SaveChangesAsync(cancellationToken);
        }

        public void Update(PostEntity entity)
        {
            context.Posts.Update(entity);
        }
    }
}
