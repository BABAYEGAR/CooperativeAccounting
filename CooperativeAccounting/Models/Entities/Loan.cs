using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CooperativeAccounting.Models.Entities
{
    public class Loan : Transport
    {
        public long LoanId { get; set; }
        [DisplayName("Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TransactionDate { get; set; }
        [DisplayName("Terminal Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? TerminalDate { get; set; }
        public string Purpose { get; set; }
        [DisplayName("Account Name")]
        [Required]
        public string AccountName { get; set; }
        [DisplayName("Account Number")]
        [Required]
        public string AccountNumber { get; set; }
        public long? TransactionTypeId { get; set; }
        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }
        public long? TransactionId { get; set; }
        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }
        [Required]
        public long? BankId { get; set; }
        [ForeignKey("BankId")]
        public Bank Bank { get; set; }
        [DisplayName("Member")]
        [Required]
        public long AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("[a-zA-Z ]*$")]
        [DisplayName("Guarantor A Name")]
        [Required]
        public string FirstGuarantorName { get; set; }
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "This field must contain only digits")]
        [DisplayName("Guarantor A Phone No.")]
        [Required]
        public string FirstGuarantorMobile { get; set; }
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("[a-zA-Z ]*$")]
        [DisplayName("Guarantor B Name")]
        [Required]
        public string SecondGuarantorName { get; set; }
        [MaxLength(100, ErrorMessage = "This field is does not support more than 100 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "This field must contain only digits")]
        [DisplayName("Guarantor B Phone No.")]
        [Required]
        public string SecondGuarantorMobile { get; set; }
        [DisplayName("Duration(In Months)")]
        [Required]
        public int Duration { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        [DisplayName("Interest Rate")]
        public double InterestRate { get; set; }
        public bool Emergency { get; set; }
    }
}
