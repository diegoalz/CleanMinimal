using CleanMinimal.Application.Contracts.Data;
using CleanMinimal.Domain.Models;
using CleanMinimal.Domain.Primitives;
using CleanMinimal.Infraestructure.Persistence;
using CleanMinimal.Infraestructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanMinimal.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<ApplicationDbContext>(
                options => options
                    .UseSqlServer(
                        configuration.GetConnectionString("SqlServer")
                    )
            );
        
        services.AddScoped<IApplicationDbcontext>(
            sp => sp.GetRequiredService<ApplicationDbContext>()
        );
        services.AddScoped<IUnitOfWork>(
            sp => sp.GetRequiredService<ApplicationDbContext>()
        );
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        return services;
    }
}