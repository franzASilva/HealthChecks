using HealthChecks.Domain.Mappers;
using HealthChecks.Domain.Models;
using HealthChecks.Domain.Repositories.Interfaces;
using HealthChecks.Domain.Services.Interfaces;

namespace HealthChecks.Domain.Services;

public class DummyService(IDummyRepository dummyRepository) : IDummyService
{
    public readonly IDummyRepository dummyRepository = dummyRepository;

    public async Task<Result> GetDummiesValuesAsync(CancellationToken ct)
    {
        return await dummyRepository.GetDummiesAsync(ct);
    }

    public async Task<Result> GetDummyAsync(int id, CancellationToken ct)
    {
        return await dummyRepository.GetDummyAsync(id, ct);
    }

    public async Task<Result> SaveDummyAsync(DummyModel dummyModel, CancellationToken ct)
    {
        var dummy = DummyMapper.ToEntity(dummyModel);
        return await dummyRepository.SaveDummyAsync(dummy, ct);
    }
}
