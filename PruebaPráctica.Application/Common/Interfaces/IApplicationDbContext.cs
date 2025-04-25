using Microsoft.EntityFrameworkCore;

namespace PruebaPráctica.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TEntity> EntitySetFor<TEntity>() where TEntity : class;
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}