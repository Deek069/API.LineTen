using Microsoft.EntityFrameworkCore;
using Domain.LineTen.Customers;
using Domain.LineTen.Products;
using Domain.LineTen.Orders;

namespace Persistence.LineTen
{
    public class LineTenDB : DbContext
    {
        public LineTenDB(DbContextOptions<LineTenDB> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LineTenDB).Assembly);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
