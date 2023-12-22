using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Orders;

namespace Persistence.LineTen.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasKey(o => o.ID);
            builder.Property(o => o.ID).ValueGeneratedNever().HasConversion(
                orderId => orderId.value,
                value => new OrderID(value)
            );
            builder.HasOne(o => o.Customer).WithMany(c => c.Orders).HasForeignKey(o => o.CustomerID);
            builder.HasOne(o => o.Product).WithMany(p => p.Orders).HasForeignKey(o => o.ProductID);
        }
    }
}
