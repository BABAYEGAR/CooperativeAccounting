using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CooperativeAccounting.Models.Entities
{
    public class Welfare : Transport
    {
        public long WelfareId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public int Duration { get; set; }
        [DisplayName("Start Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [DisplayName("Terminal Date")]
        [Required]
        public DateTime TerminalDate { get; set; }
        [Required]
        public double Amount { get; set; }
        [DisplayName("Welfare Owner")]
        public string Owner { get; set; }
        [DisplayName("Member")]
        public long? AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        public bool Member { get; set; }
    }
}
