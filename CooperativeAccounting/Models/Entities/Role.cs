namespace CooperativeAccounting.Models.Entities
{
    public class Role : Transport
    {
        public long RoleId { get; set; }
        public string Name { get; set; }
        public bool ManageMembers { get; set; }
        public bool ManageMemberTransaction { get; set; }
        public bool ManageAllTransaction { get; set; }
        public bool ManageMemberRoles { get; set; }
        public bool ManageTransactionType { get; set; }

    }
}
