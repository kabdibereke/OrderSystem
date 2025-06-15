namespace OrderSystem.Domain.Common.Repositories;

public interface  IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}