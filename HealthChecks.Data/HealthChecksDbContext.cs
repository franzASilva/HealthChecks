using HealthChecks.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthChecks.Data;

public class HealthChecksDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "HealthChecksDb");
    }

    public DbSet<Dummy> Dummies { get; set; }
}
