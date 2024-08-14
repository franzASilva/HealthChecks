using HealthChecks.Domain.Entities;
using HealthChecks.Domain.Models;

namespace HealthChecks.Domain.Mappers;

public static class DummyMapper
{
    public static Dummy ToEntity(DummyModel dummyModel)
    {
        return new Dummy
        {
            Date = dummyModel.Date,
            Value = dummyModel.Value,
            ValueOffset = dummyModel.ValueOffset
        };
    }
}
