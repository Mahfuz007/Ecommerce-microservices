﻿// <auto-generated />
using Discount.Grpc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Discount.Grpc.Migrations
{
    [DbContext(typeof(DiscountContext))]
    partial class DiscountContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("Discount.Grpc.Entities.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 100m,
                            Description = "Discount for iphone.",
                            ProductName = "Iphone 15"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 75m,
                            Description = "Discount for Android.",
                            ProductName = "Samsung S24 Ultra"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
