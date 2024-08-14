using HealthChecks.Domain.Models;

namespace HealthChecks.Domain.Services.Interfaces;

public interface IDummyService
{
    Task<Result> GetDummiesValuesAsync(CancellationToken ct);
    Task<Result> GetDummyAsync(int id, CancellationToken ct);
    Task<Result> SaveDummyAsync(DummyModel dummyModel, CancellationToken ct);
}
