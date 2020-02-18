using System;
using System.ComponentModel.DataAnnotations;

namespace DrewKestellSite.Models
{
    public class SiteVisit
    {
        public int Id { get; set; }

        [Required, MaxLength(128)]
        public string Url { get; set; }

        [Required, MaxLength(128)]
        public string IPAddress { get; set; }

        public DateTime DateTime { get; set; }
    }
}
