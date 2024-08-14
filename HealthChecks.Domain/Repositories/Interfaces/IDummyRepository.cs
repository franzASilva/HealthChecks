using HealthChecks.Domain.Entities;

namespace HealthChecks.Domain.Repositories.Interfaces;

public interface IDummyRepository
{
    Task<Result> GetDummiesAsync(CancellationToken ct);
    Task<Result> GetDummyAsync(int id, CancellationToken ct);
    Task<Result> SaveDummyAsync(Dummy dummy, CancellationToken ct);
}
