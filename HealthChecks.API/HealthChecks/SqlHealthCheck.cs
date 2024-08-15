using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.API.HealthChecks;

public class SqlHealthCheck(IConfiguration configuration) : IHealthCheck
{
    private readonly string connectionString = configuration.GetConnectionString("HealthCheckDatabase") ?? string.Empty;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = default)
    {
        try
        {
            using var sqlConnection = new SqlConnection(connectionString);

            await sqlConnection.OpenAsync(ct);

            using var command = sqlConnection.CreateCommand();
            command.CommandText = "select 1";

            await command.ExecuteScalarAsync(ct);

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(context.Registration.FailureStatus.ToString(), exception: ex);
        }
    }
}

