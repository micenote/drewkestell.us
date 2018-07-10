using BloogBotBook.Models;
using Microsoft.EntityFrameworkCore;

namespace BloogBotBook.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Article> Articles { get; set; }

        public DbSet<ArticleChapter> ArticleChapters { get; set; }

        public DbSet<ArticleComment> ArticleComments { get; set; }

        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasIndex(a => a.Slug)
                .IsUnique();

            modelBuilder.Entity<ArticleChapter>()
                .HasIndex(b => new { b.ArticleId, b.Value })
                .IsUnique();
        }
    }
}
