﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CooperativeAccounting.Models.Entities
{
    public class Transaction : Transport
    {
        public long TransactionId { get; set; }
        [DisplayName("Transaction Type")]
        public long TransactionTypeId { get; set; }
        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }
        [DisplayName("Transaction Name/Item")]
        public string TransactionName { get; set; }
        public double Amount { get; set; }
        public string Action { get; set; }
        [DisplayName("Member")]
        public long AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }
        [DisplayName("Transaction Date")]
        public DateTime? TransactionDate { get; set; }
        public string VoucherNumber { get; set; }
    }
}
