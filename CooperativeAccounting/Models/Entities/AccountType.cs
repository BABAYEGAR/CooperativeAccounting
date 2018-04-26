using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CooperativeAccounting.Models.Entities
{
    public class AccountType : Transport
    {
        public long AccountTypeId { get; set; }
        public string Name { get; set; }
        public bool IncreaseCredit { get; set; }
        public bool IncreaseDebit { get; set; }
    }
}
