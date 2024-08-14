using HealthChecks.Data.Repositories;
using HealthChecks.Domain.Repositories.Interfaces;
using HealthChecks.Domain.Services;
using HealthChecks.Domain.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HealthChecks.IoC;

public static class Bootstrapper
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDummyService, DummyService>();
        services.AddScoped<IDummyRepository, DummyRepository>();

        return services;
    }
}