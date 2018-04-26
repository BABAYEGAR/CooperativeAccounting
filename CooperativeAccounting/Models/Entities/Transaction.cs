using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CooperativeAccounting.Models.Entities
{
    public class Transaction : Transport
    {
        public long TransactionId { get; set; }
        public long AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
        public string TransactionName { get; set; }
        public double Amount { get; set; }
        public string Action { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
