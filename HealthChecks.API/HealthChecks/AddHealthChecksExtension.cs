using HealthChecks.Domain.Settings;
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
            .AddSqlServer(connectionString ?? string.Empty, healthQuery: "select 1", name: "SQL Server", failureStatus: HealthStatus.Unhealthy)
            .AddCheck<RemoteHealthCheck>("Remote endpoints Health Check")
            .AddCheck<InMemoryDbHealthCheck>("In memory database Health Check")
            .AddCheck<SqlHealthCheck>("Custom-sql", HealthStatus.Unhealthy)
            .AddCheck<ServerStatusHealthCheck>("ping status")
            .AddCheck<CustomHealthCheck>("Cutom health check")
            .AddUrlGroup(new Uri("https://www.google.com"), name: "Another uri healthcheck", failureStatus: HealthStatus.Unhealthy);

        // others:
        //
        // SQL Server -AspNetCore.HealthChecks.SqlServer
        // Postgres - AspNetCore.HealthChecks.Npgsql
        // Redis - AspNetCore.HealthChecks.Redis
        // RabbitMQ - AspNetCore.HealthChecks.RabbitMQ
        // AWS S3 -AspNetCore.HealthChecks.Aws.S3
        // SignalR - AspNetCore.HealthChecks.SignalR
        // Uris - AspNetCore.HealthChecks.Uris
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

        var result = JsonSerializer.Serialize(new
        {
            statusApplication = healthReport.Status.ToString(),
            healthChecks = healthReport.Entries.Select(e => new
            {
                check = e.Key,
                status = e.Value.Status.ToString(),
                errorMessage = e.Value.Exception?.Message,
                duration_ms = e.Value.Duration.Milliseconds,
                description = e.Value.Description
            })
        }, SerializerSettings.Default);

        return context.Response.WriteAsync(result);
    }
}
