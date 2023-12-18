using Domain.LineTen.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.LineTen.Configurations
{
    internal class CustomerConfiguration: IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder) 
        {
            builder.ToTable("Customer");
            builder.HasKey(c => c.ID);
            builder.Property(c => c.ID).ValueGeneratedNever().HasConversion(
                customerId => customerId.value,
                value => new CustomerID(value)
            );
            builder.Property(c => c.FirstName).HasMaxLength(50);
            builder.Property(c => c.LastName).HasMaxLength(50);
            builder.Property(c => c.Phone).HasMaxLength(20);
            builder.Property(c => c.Email).HasMaxLength(100);
        }
    }
}
