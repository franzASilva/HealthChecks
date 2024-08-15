using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.API.HealthChecks;

public class RemoteHealthCheck(IHttpClientFactory httpClientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = new CancellationToken())
    {
        using var httpClient = httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync("https://api.ipify.org", ct);

        if (response.IsSuccessStatusCode)
        {
            return HealthCheckResult.Healthy("Remote endpoints is healthy.");
        }

        return HealthCheckResult.Unhealthy("Remote endpoint is unhealthy");
    }
}
