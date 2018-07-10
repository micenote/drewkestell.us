using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloogBotBook.Models
{
    public class Article
    {
        public Article()
        {
            ArticleChapters = new HashSet<ArticleChapter>();
            Comments = new HashSet<ArticleComment>();
        }

        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Slug { get; set; }

        public virtual ICollection<ArticleChapter> ArticleChapters { get; set; }

        public virtual ICollection<ArticleComment> Comments { get; set; }
    }
}
