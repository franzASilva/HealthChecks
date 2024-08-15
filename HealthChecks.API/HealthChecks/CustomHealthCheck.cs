using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.API.HealthChecks;

public class CustomHealthCheck(IHttpClientFactory httpClientFactory) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = new CancellationToken())
    {
        using var httpClient = httpClientFactory.CreateClient();
        List<string> hChecks = [
            "https://example.com.br/api/healthcheck",
            "https://api.example.com/healthcheck"
        ];

        var tasks = hChecks.Select(hc => httpClient.GetAsync(hc, ct));
        var responses = await Task.WhenAll(tasks);

        if (responses is not null && responses.All(r => r.IsSuccessStatusCode))
        {
            var tasksResp = responses.Select(r => r.Content.ReadAsStringAsync(ct));
            var contents = await Task.WhenAll(tasksResp);

            if (contents is not null && (contents.Select(c => c.Replace("\"", string.Empty).Contains("statusApplication:Healthy")).Count() == contents.Length))
            {
                return HealthCheckResult.Healthy("Health checks is healthies.");
            }
        }

        return HealthCheckResult.Unhealthy("Health checks is unhealthies");
    }
}
