using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<TEntity> EntitySetFor<TEntity>() where TEntity : class => Set<TEntity>();
}