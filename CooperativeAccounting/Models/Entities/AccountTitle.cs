using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CooperativeAccounting.Models.Entities
{
    public class AccountTitle : Transport
    {
        public long AccountTitleId { get; set; }
        public string Name { get; set; }
    }
}
