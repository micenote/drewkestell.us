using DrewKestellSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace DrewKestellSite.ViewModels
{
    public class ArticleViewModel
    {
        public ArticleViewModel()
        {
            ArticleChapters = new List<ArticleChapterViewModel>();
        }

        public ArticleViewModel(Article article)
        {
            Id = article.Id;
            Description = article.Description;
            ThumbnailUrl = article.ThumbnailUrl;
            Slug = article.Slug;
            Title = article.Title;
            ArticleChapters = article.ArticleChapters
                .OrderBy(c => c.ChapterNumber)
                .Select(a => new ArticleChapterViewModel(a));
        }

        public int Id { get; }

        public string Slug { get; }

        public string Title { get; }

        public string Description { get; }

        public string ThumbnailUrl { get; }

        public IEnumerable<ArticleChapterViewModel> ArticleChapters { get; }
    }
}
