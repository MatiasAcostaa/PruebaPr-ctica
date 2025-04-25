using Microsoft.EntityFrameworkCore;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Infrastructure.Persistence;

namespace Application.IntegrationTests;

public class IntegrationTestFixture : IDisposable
{
    public IApplicationDbContext DbContext { get; private set; }

    public IntegrationTestFixture()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .Options;

        DbContext = new ApplicationDbContext(options);
    }

    public void Dispose()
    {
        ((ApplicationDbContext)DbContext).Database.EnsureDeleted();
        ((ApplicationDbContext)DbContext).Dispose();
    }
}