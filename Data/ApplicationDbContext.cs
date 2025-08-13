using CollegeProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace CollegeProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator == null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<User> User { get; set; }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<WareHouse> WareHouse { get; set; }

        public DbSet<WareHouseManager> WareHouseManager { get; set; }

        public DbSet<Supplier> Supplier { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<SupplierOrder> SupplierOrder { get; set; }

        public DbSet<WareHouseManagerOrder> WareHouseManagerOrder { get;set; }

        public DbSet<DeliveryPerson> DeliveryPerson { get; set; }
    }
}
