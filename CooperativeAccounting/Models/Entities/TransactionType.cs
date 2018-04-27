using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CooperativeAccounting.Models.Entities
{
    public class TransactionType : Transport
    {
        public long TransactionTypeId { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Credit Transaction?")]
        public bool Credit { get; set; }
        [DisplayName("Debit Transaction??")]
        public bool Debit { get; set; }
        [DisplayName("Cash Transaction?")]
        public bool Cash { get; set; }
        [DisplayName("Asset Transaction?")]
        public bool Asset { get; set; }
        [DisplayName("Equity Transaction?")]
        public bool Equity { get; set; }
        [DisplayName("Liability Transaction?")]
        public bool Liability { get; set; }
    }
}
