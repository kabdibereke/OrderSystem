using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderSystem.Domain.Customers.Entities;
using OrderSystem.Domain.Customers.ValueObjects;

namespace OrderSystem.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration: IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(id => id.Value, value => CustomerId.FromGuid(value));

        builder.OwnsOne(p => p.Email, email =>
        {
            email.Property(n => n.Value).HasColumnName("Email");
        });
        
        builder.Property(p => p.FullName)
            .IsRequired()
            .HasMaxLength(100);
        

        builder.ToTable("Customers");
    }
}