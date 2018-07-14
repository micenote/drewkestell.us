using DrewKestellSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DrewKestellSite.ViewModels
{
    public class ArticleChapterViewModel
    {
        public ArticleChapterViewModel() { }

        public ArticleChapterViewModel(ArticleChapter chapter)
        {
            ArticleId = chapter.Article.Id;
            ArticleTitle = chapter.Article.Title;
            Name = chapter.Name;
            Text = chapter.Text;
            ChapterNumber = chapter.ChapterNumber;
            ChapterNavigation = chapter.Article.ArticleChapters
                .OrderBy(c => c.ChapterNumber)
                .Select(c => Tuple.Create(c.ChapterNumber, c.Name, c.ChapterNumber == chapter.ChapterNumber));
            Comments = chapter.Article.Comments
                .Where(c => c.Status == "approved")
                .Select(c => new ArticleCommentViewModel(c));
        }
        
        public int ArticleId { get; }

        public string ArticleTitle { get; }

        public string Name { get; }

        public string Text { get; }

        public int ChapterNumber { get; }

        public IEnumerable<Tuple<int, string, bool>> ChapterNavigation { get; }

        public IEnumerable<ArticleCommentViewModel> Comments { get; }
    }
}
