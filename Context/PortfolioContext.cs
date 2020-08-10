using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PersonalPortfolio.Context
{
    public class PortfolioContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Category> Category { get; set; }

        public PortfolioContext(DbContextOptions<PortfolioContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<Post>().ToTable("Posts");
            modelBuilder.Entity<Category>().ToTable("Category");
        }
    }

    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public List<Technology> Technologies { get; set; }
    }

    public class Technology
    {
        public int TechnologyId { get; set; }
        public string Name { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Descriptiong { get; set; }

        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
    }
}
