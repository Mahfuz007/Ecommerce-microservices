using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
                orderItemId => orderItemId.Value,
                dbid => OrderItemId.Of(dbid)
            );
        builder.HasOne(o => o.ProductId)
            .WithMany()
            .HasForeignKey(o => o.ProductId);
        builder.Property(o => o.Quantity).IsRequired();
        builder.Property(o => o.Price).IsRequired();
    }
}
