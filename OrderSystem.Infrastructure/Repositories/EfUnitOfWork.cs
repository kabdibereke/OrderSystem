using OrderSystem.Domain.Common.Repositories;
using OrderSystem.Infrastructure.Persistence.DbContexts;

namespace OrderSystem.Infrastructure.Repositories;

public class EfUnitOfWork:IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public EfUnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}