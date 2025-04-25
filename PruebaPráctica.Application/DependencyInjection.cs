using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PruebaPráctica.Application.Common.Behaviours;


namespace PruebaPráctica.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly, includeInternalTypes: true);
        
        return services;
    }
}