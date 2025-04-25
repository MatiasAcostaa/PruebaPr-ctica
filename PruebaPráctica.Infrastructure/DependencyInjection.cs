using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaPráctica.Application.Common.Interfaces;
using PruebaPráctica.Infrastructure.Persistence;

namespace PruebaPráctica.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database") 
                                  ?? throw new InvalidOperationException("La cadena de conexión no se ha configurado correctamente.");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<DatabaseInitializer>();
    }
}