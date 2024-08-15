using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace HealthChecks.API.HealthChecks;

public class ServerStatusHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = new CancellationToken())
    {
        var pingSender = new Ping();
        var reply = await pingSender.SendPingAsync("www.google.com", TimeSpan.FromSeconds(1), cancellationToken: ct);

        if (reply.Status == IPStatus.Success)
        {
            return HealthCheckResult.Healthy("server status is healthy.");
        }

        return HealthCheckResult.Unhealthy(reply.Status.ToString());
    }
}
