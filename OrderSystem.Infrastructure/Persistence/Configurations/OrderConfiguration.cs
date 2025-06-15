using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain.Catalog.ValueObjects;
using OrderSystem.Domain.Customers.Entities;
using OrderSystem.Domain.Customers.ValueObjects;
using OrderSystem.Domain.Orders.Entities;
using OrderSystem.Domain.Orders.ValueObjects;

namespace OrderSystem.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    { builder.ToTable("Orders");
        
        builder.HasKey(o => o.Id);
        
        builder.Property(o => o.Id)
            .HasConversion(id => id.Value, value => OrderId.FromGuid(value));
        
        builder.Property(o => o.CustomerId)
            .HasConversion(id => id.Value, value => CustomerId.FromGuid(value))
            .IsRequired();
        
        builder.OwnsOne(o => o.Status, status =>
        {
            status.Property(s => s.Value)
                .HasColumnName("Status")
                .IsRequired()
                .HasMaxLength(32);
        });
        
        builder.Property(o => o.CreatedAt)
            .IsRequired();
        
        builder.OwnsMany(o => o.Items, item =>
        {
            item.WithOwner().HasForeignKey("OrderId"); 
            item.Property(i => i.ProductId)
                .HasConversion(id => id.Value, value => ProductId.FromGuid(value))
                .HasColumnName("ProductId");

            item.Property(i => i.ProductName)
                .HasColumnName("ProductName")
                .IsRequired()
                .HasMaxLength(100);

            item.Property(i => i.Quantity)
                .HasColumnName("Quantity")
                .IsRequired();

            item.Property(i => i.UnitPrice)
                .HasColumnName("UnitPrice")
                .IsRequired();

            item.ToTable("OrderItems"); 
        });
    }
}