using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloogBotBook.Models
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

        [ForeignKey("Article")]
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
