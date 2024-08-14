using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;

namespace HealthChecks.API.HealthChecks;

public static class AddHealthChecksExtension
{
    public static void AddApiHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("HealthCheckDatabase");

        services
            .AddHealthChecks()
            .AddSqlServer(connectionString ?? string.Empty, healthQuery: "select 1", name: "SQL servere", failureStatus: HealthStatus.Unhealthy)
            .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check")
            .AddCheck<InMemoryDbHealthCheck>("In memory database Health Check");
    }

    public static void DefineHealthCheckEndpoint(this WebApplication app)
    {
        app.UseHealthChecks(
            "/healthcheck",
            new HealthCheckOptions { ResponseWriter = CustomResponseWriter }
        );
    }

    private static Task CustomResponseWriter(HttpContext context, HealthReport healthReport)
    {
        context.Response.ContentType = "application/json";

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        var result = JsonSerializer.Serialize(new
        {
            statusApplication = healthReport.Status.ToString(),
            healthChecks = healthReport.Entries.Select(e => new
            {
                check = e.Key,
                status = e.Value.Status.ToString(),
                errorMessage = e.Value.Exception?.Message
            })
        }, options);

        return context.Response.WriteAsync(result);
    }
}
