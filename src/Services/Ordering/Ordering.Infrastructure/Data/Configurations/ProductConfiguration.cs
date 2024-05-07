using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
                productId => productId.Value,
                dbid => ProductId.Of(dbid)
            );
        builder.Property(o => o.Name).HasMaxLength(100).IsRequired();
        builder.Property(o => o.Price).IsRequired();
    }
}
