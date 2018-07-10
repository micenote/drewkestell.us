using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloogBotBook.Models
{
    public class ArticleChapter
    {
        public int Id { get; set; }

        [Range(1, Int32.MaxValue)]
        public int Value { get; set; }

        [Required, MaxLength(128)]
        public string LinkText { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
