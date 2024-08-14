using HealthChecks.Domain;
using HealthChecks.Domain.Entities;
using HealthChecks.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HealthChecks.Data.Repositories;

public class DummyRepository : IDummyRepository
{
    public readonly List<Task> taskList = [];

    public DummyRepository()
    {
        using var context = new HealthChecksDbContext();

        // zzzZZZZZzzzz 🐢
        if (context.Dummies.ToList().Count <= 0)
        {
            var dummies = Enumerable
                .Range(1, 5)
                .Select(index =>
                new Dummy
                {
                    Date = DateTime.Now.AddDays(index),
                    Value = Random.Shared.Next(-20, 55),
                    ValueOffset = Random.Shared.Next(1, 100)
                })
            .ToArray();

            context.Dummies.AddRange(dummies);
            context.SaveChanges();
        }
    }

    public async Task<Result> GetDummiesAsync(CancellationToken ct)
    {
        using var context = new HealthChecksDbContext();
        var dummies = await context.Dummies.ToListAsync(ct);
        return new Result(dummies.Count > 0 ? dummies : "Dummies Not Found!!!");
    }

    public async Task<Result> GetDummyAsync(int id, CancellationToken ct)
    {
        using var context = new HealthChecksDbContext();
        var dummy = await context.Dummies.FirstOrDefaultAsync(d => d.Id == id, ct);
        return new Result(dummy is not null ? dummy : "Dummy Not Found!!!");
    }

    public async Task<Result> SaveDummyAsync(Dummy dummy, CancellationToken ct)
    {
        using var context = new HealthChecksDbContext();
        var dummyRet = await context.Dummies.AddAsync(dummy, ct);
        var task = context.SaveChangesAsync(ct);
        Task.WaitAll(task);
        return new Result(dummyRet is not null ? dummyRet.Entity : "Dummy wasn't saved");
    }
}
