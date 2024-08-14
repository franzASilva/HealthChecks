namespace HealthChecks.Domain.Records;

public sealed record DummyRecord(DateOnly Date, int Value, int Id = 0)
{
    public int ValueOffset => 32 + (int)(Value / 0.5556);
}