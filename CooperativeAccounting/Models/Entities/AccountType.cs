using System.ComponentModel;

namespace CooperativeAccounting.Models.Entities
{
    public class AccountType : Transport
    {
        public long AccountTypeId { get; set; }
        public string Name { get; set; }
        [DisplayName("Credit Transaction?")]
        public bool Credit { get; set; }
        [DisplayName("Debit Transaction??")]
        public bool Debit { get; set; }
        public bool Cash { get; set; }
    }
}
