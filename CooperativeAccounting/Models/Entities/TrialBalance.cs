using System;

namespace CooperativeAccounting.Models.Entities
{
    public class TrialBalance : Transport
    {
        public long TrialBalanceId { get; set; }
        public string Action { get; set; }
        public DateTime Date { get; set; }
    }
}
