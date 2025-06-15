using Microsoft.EntityFrameworkCore;
using OrderSystem.Domain.Products.Entities;
using OrderSystem.Domain.Customers.Entities;
using OrderSystem.Domain.Orders.Entities;

namespace OrderSystem.Infrastructure.Persistence.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Order> Orders => Set<Order>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}