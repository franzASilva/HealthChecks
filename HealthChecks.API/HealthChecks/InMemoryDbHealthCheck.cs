﻿using HealthChecks.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthChecks.API.HealthChecks;

public class InMemoryDbHealthCheck : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken ct = new CancellationToken())
    {
        using var dbContext = new HealthChecksDbContext();
        var dummy = await dbContext.Dummies.FirstOrDefaultAsync(ct);

        if (dummy is not null)
        {
            return HealthCheckResult.Healthy("In memory db is healthy.");
        }

        return HealthCheckResult.Unhealthy("In memory db is unhealthy");
    }
}
