using Microsoft.EntityFrameworkCore;
using PersonalPortfolio.Context.Entity;

namespace PersonalPortfolio.Context
{
    public class PortfolioContext : DbContext
    {
        public DbSet<ProjectEntity> Projects { get; set; }
        public DbSet<TechnologyEntity> Technologies { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }

        public PortfolioContext(DbContextOptions<PortfolioContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>().ToTable("Project");
            modelBuilder.Entity<TechnologyEntity>().ToTable("Technology"); 
            modelBuilder.Entity<PostEntity>().ToTable("Posts");
            modelBuilder.Entity<CategoryEntity>().ToTable("Category");
        }
    }
}
