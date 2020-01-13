using DrewKestellSite.Models;
using Microsoft.EntityFrameworkCore;

namespace DrewKestellSite.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleChapter> ArticleChapters { get; set; }

        public DbSet<ArticleComment> ArticleComments { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<SiteVisit> SiteVisits { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasIndex(a => a.Slug)
                .IsUnique();

            modelBuilder.Entity<Article>()
                .HasIndex(a => a.Title)
                .IsUnique();

            modelBuilder.Entity<ArticleChapter>()
                .HasIndex(b => new { b.ArticleId, b.ChapterNumber })
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();
        }
    }
}
