using System;
using System.ComponentModel.DataAnnotations;

namespace CooperativeAccounting.Models.Entities
{
    public class Minute : Transport
    {
        public long MinuteId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
