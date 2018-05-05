using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CooperativeAccounting.Models.Entities
{
    public class AppTransport
    {
        public List<Transaction> Transactions { get; set; }
        public List<AppUser> AppUsers { get; set; }
        public decimal CurrentBalance { get; set; }
    }
}
