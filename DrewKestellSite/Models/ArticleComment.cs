using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DrewKestellSite.Models
{
    public class ArticleComment
    {
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Name { get; set; }

        [Required, MaxLength(5000)]
        public string Message { get; set; }

        [MaxLength(5000)]
        public string Response { get; set; }

        [Required, MaxLength(32)]
        public string Status { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
