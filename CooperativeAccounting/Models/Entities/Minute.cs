using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CooperativeAccounting.Models.Entities;

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
