using System;

namespace CooperativeAccounting.Models.Entities
{
    public class CashBook : Transport
    {
        public long CashBookId { get; set; }
        public double Amount { get; set; }
        public string VoucherNumber { get; set; }
        public DateTime Date { get; set; }
    }
}
