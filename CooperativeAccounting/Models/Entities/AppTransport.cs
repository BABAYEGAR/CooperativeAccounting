using System.Collections.Generic;

namespace CooperativeAccounting.Models.Entities
{
    public class AppTransport
    {
        public List<Transaction> Transactions { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
