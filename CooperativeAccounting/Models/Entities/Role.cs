using System.ComponentModel.DataAnnotations;

namespace CooperativeAccounting.Models.Entities
{
    public class Role : Transport
    {
        public long RoleId { get; set; }
        [Required]
        public string Name { get; set; }
        public bool ManageMembers { get; set; }
        public bool ManageMemberTransaction { get; set; }
        public bool ManageAllTransaction { get; set; }
        public bool ManageMemberRoles { get; set; }
        public bool ManageTransactionType { get; set; }
        public bool ManageLoan { get; set; }
        public bool ManageWelfare { get; set; }
        public bool ManageMinute { get; set; }
        public bool ManageCashTransaction { get; set; }

    }
}
