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
        public DbSet<TagEntity> Tags { get; set; }

        public PortfolioContext(DbContextOptions<PortfolioContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectEntity>().ToTable("Project")
                                                    .HasMany(x => x.Technologies)
                                                    .WithOne(x => x.Project)
                                                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PostEntity>().ToTable("Posts")
                                                .HasOne(x => x.Category)
                                                .WithOne(x => x.Post)
                                                .HasForeignKey<CategoryEntity>(x => x.CategoryId);

            //modelBuilder.Entity<TechnologyEntity>().ToTable("Technology"); 
            //modelBuilder.Entity<CategoryEntity>().ToTable("Category");
            //modelBuilder.Entity<TagEntity>().ToTable("Tags");
        }
    }
}
