using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrewKestellSite.Models
{
    public class ArticleChapter
    {
        public int Id { get; set; }

        [Range(1, Int32.MaxValue)]
        public int ChapterNumber { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required]
        public string Text { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
