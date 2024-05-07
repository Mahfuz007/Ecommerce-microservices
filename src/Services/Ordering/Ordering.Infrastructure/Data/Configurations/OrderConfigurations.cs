using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasConversion(
                orderId => orderId.Value,
                dbid => OrderId.Of(dbid)
            );
        builder.HasMany(o => o.OrderItems)
            .WithOne()
            .HasForeignKey(o => o.OrderId);
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .IsRequired();
        builder.ComplexProperty(
            o => o.Name, nameBuilder =>
            {
                nameBuilder.Property(oi => oi.Value)
                        .HasColumnName(nameof(Order.Name))
                        .HasMaxLength(100)
                        .IsRequired();
            });
        builder.ComplexProperty(
            o => o.ShippingAddress, addressBuilder =>
            {
                addressBuilder.Property(oi => oi.FirstName)
                            .HasMaxLength(100)
                            .IsRequired();

                addressBuilder.Property(oi => oi.LastName)
                            .HasMaxLength(100)
                            .IsRequired();

                addressBuilder.Property(oi => oi.AddressLine)
                            .HasMaxLength(100)
                            .IsRequired();

                addressBuilder.Property(oi => oi.City)
                            .HasMaxLength(15);

                addressBuilder.Property(oi => oi.Email)
                            .IsRequired();

                addressBuilder.Property(oi => oi.State)
                            .HasMaxLength(15);

                addressBuilder.Property(oi => oi.ZipCode)
                            .IsRequired();

                addressBuilder.Property(oi => oi.Country)
                            .HasMaxLength(15);
            });

        builder.ComplexProperty(
            o => o.BillingAddress, addressBuilder =>
            {
                addressBuilder.Property(oi => oi.FirstName)
                            .HasMaxLength(100)
                            .IsRequired();

                addressBuilder.Property(oi => oi.LastName)
                            .HasMaxLength(100)
                            .IsRequired();

                addressBuilder.Property(oi => oi.AddressLine)
                            .HasMaxLength(100)
                            .IsRequired();

                addressBuilder.Property(oi => oi.City)
                            .HasMaxLength(15);

                addressBuilder.Property(oi => oi.Email)
                            .IsRequired();

                addressBuilder.Property(oi => oi.State)
                            .HasMaxLength(15);

                addressBuilder.Property(oi => oi.ZipCode)
                            .IsRequired();

                addressBuilder.Property(oi => oi.Country)
                            .HasMaxLength(15);
            });

        builder.ComplexProperty(
               o => o.PaymentDetails, paymentBuilder =>
               {
                   paymentBuilder.Property(p => p.Name)
                       .HasMaxLength(50);

                   paymentBuilder.Property(p => p.CardNumber)
                       .HasMaxLength(24)
                       .IsRequired();

                   paymentBuilder.Property(p => p.ExpiryDate)
                       .HasMaxLength(10);

                   paymentBuilder.Property(p => p.CVV)
                       .HasMaxLength(3);

                   paymentBuilder.Property(p => p.PaymentMethod);
               });

        builder.Property(o => o.Status)
            .HasDefaultValue(OrderStatus.Draft)
            .HasConversion(
                s => s.ToString(),
                dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

        builder.Property(o => o.TotalPrice);

    }
}