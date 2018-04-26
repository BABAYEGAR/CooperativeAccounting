using CooperativeAccounting.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CooperativeAccounting.Models.DataBaseConnections
{
    public class CooperativeAccountingDataContext : DbContext
    {
        // Your context has been configured to use a 'VmsDataContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // ' Imager.DataManager.DataBaseConnections.VMSConnections' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ImageDataContext' 
        // connection string in the application configuration file.
        public CooperativeAccountingDataContext(DbContextOptions<CooperativeAccountingDataContext> options)
            : base(options)
        {
        }
        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.
        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<TransactionType> AccountTypes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=.;Database=CamerackMarket;Trusted_Connection=True;");
        }
    }
}