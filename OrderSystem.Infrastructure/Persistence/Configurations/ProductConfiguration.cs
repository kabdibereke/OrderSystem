using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain.Products.Entities;
using OrderSystem.Domain.Catalog.ValueObjects;

namespace OrderSystem.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => ProductId.FromGuid(value));

        builder.OwnsOne(p => p.Name, name =>
        {
            name.Property(n => n.Value).HasColumnName("Name");
        });

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(p => p.Value).HasColumnName("Price");
        });

        builder.ToTable("Products");
    }
}