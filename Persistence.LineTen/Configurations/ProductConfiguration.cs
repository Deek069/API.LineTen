using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.LineTen.Entities;
using Domain.LineTen.ValueObjects.Products;

namespace Persistence.LineTen.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(p => p.ID);
            builder.Property(p => p.ID).ValueGeneratedNever().HasConversion(
                productId => productId.value,
                value => new ProductID(value)
            );
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.SKU).HasMaxLength(20);
        }
    }
}
