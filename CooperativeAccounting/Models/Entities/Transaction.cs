using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace CooperativeAccounting.Models.Entities
{
    public class Transaction : Transport
    {
        public long TransactionId { get; set; }
        [Required]
        [DisplayName("Transaction Type")]
        public long TransactionTypeId { get; set; }
        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }
        [Required]
        [DisplayName("Transaction Name/Item")]
        public string TransactionName { get; set; }
        [Required]
        public double Amount { get; set; }
        public string Action { get; set; }
        [DisplayName("Member")]
        [Required]
        public long AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        [DisplayName("Transaction Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransactionDate { get; set; }
        public string VoucherNumber { get; set; }
    }
}
