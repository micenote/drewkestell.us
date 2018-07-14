using System.Collections.Generic;

namespace DrewKestellSite.FormObjects
{
    public class ArticleFormObject
    {
        public string Slug { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ThumbnailUrl { get; set; }

        public IEnumerable<ArticleChapterFormObject> ArticleChapters { get; set; }
    }
}
