using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PruebaPráctica.Domain.Entities;

namespace PruebaPráctica.Infrastructure.Persistence;

public sealed class DatabaseInitializer(
    ApplicationDbContext dbContext,
    ILogger<DatabaseInitializer> logger
)
{
    public async Task InitializeAsync()
    {
        try
        {
            await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        await using IDbContextTransaction transaction = await dbContext.Database.BeginTransactionAsync();
        
        try
        {
            var admin = new User
            {
                Username = "testuser",
                PasswordHash = "$2a$12$DMmsE2FvRpZn0UPC8tu7KOWMcauonajx9pSUvAaK5xZiuOojCWGUW"
            };
            
            if (!dbContext.Set<User>().Any(user => user.Username == admin.Username))
            {
                dbContext.Set<User>().Add(admin);
                            
                await dbContext.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }
}