using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class CustomerConfigurations : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
             customerId => customerId.Value,
             dbid => CustomerId.Of(dbid)
            );
        builder.Property(o => o.Name).HasMaxLength(100).IsRequired();
        builder.Property(o => o.Email).IsRequired();
    }
}
