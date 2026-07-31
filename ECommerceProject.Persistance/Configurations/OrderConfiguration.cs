using ECommerceProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Persistance.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, sa =>
            {
                sa.Property(a => a.FirstName).HasColumnName("Shipping_FirstName").IsRequired().HasMaxLength(100);
                sa.Property(a => a.LastName).HasColumnName("Shipping_LastName").IsRequired().HasMaxLength(100);
                sa.Property(a => a.PhoneNumber).HasColumnName("Shipping_PhoneNumber").IsRequired().HasMaxLength(20);
                sa.Property(a => a.City).HasColumnName("Shipping_City").IsRequired().HasMaxLength(100);
                sa.Property(a => a.District).HasColumnName("Shipping_District").IsRequired().HasMaxLength(100);
                sa.Property(a => a.FullAddress).HasColumnName("Shipping_FullAddress").IsRequired().HasMaxLength(500);

                // Teslimat adresinde fatura bilgileri olmayabilir ama modelde olduğu için mapplemek gerekiyor
                sa.Property(a => a.InvoiceType).HasColumnName("Shipping_InvoiceType").IsRequired().HasMaxLength(50);
                sa.Property(a => a.CompanyName).HasColumnName("Shipping_CompanyName").HasMaxLength(200);
                sa.Property(a => a.TaxOffice).HasColumnName("Shipping_TaxOffice").HasMaxLength(50);
                sa.Property(a => a.TaxNumber).HasColumnName("Shipping_TaxNumber").HasMaxLength(100);
            });

            builder.OwnsOne(o => o.BillingAddress, ba =>
            {
                ba.Property(a => a.FirstName).HasColumnName("Billing_FirstName").IsRequired().HasMaxLength(100);
                ba.Property(a => a.LastName).HasColumnName("Billing_LastName").IsRequired().HasMaxLength(100);
                ba.Property(a => a.PhoneNumber).HasColumnName("Billing_PhoneNumber").IsRequired().HasMaxLength(20);
                ba.Property(a => a.City).HasColumnName("Billing_City").IsRequired().HasMaxLength(100);
                ba.Property(a => a.District).HasColumnName("Billing_District").IsRequired().HasMaxLength(100);
                ba.Property(a => a.FullAddress).HasColumnName("Billing_FullAddress").IsRequired().HasMaxLength(500);

                ba.Property(a => a.InvoiceType).HasColumnName("Billing_InvoiceType").IsRequired().HasMaxLength(50);
                ba.Property(a => a.CompanyName).HasColumnName("Billing_CompanyName").HasMaxLength(200);
                ba.Property(a => a.TaxOffice).HasColumnName("Billing_TaxOffice").HasMaxLength(50);
                ba.Property(a => a.TaxNumber).HasColumnName("Billing_TaxNumber").HasMaxLength(100);
            });
        }
    }
}
