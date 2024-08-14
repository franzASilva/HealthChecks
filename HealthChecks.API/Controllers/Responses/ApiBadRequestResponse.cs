namespace HealthChecks.API.Controllers.Responses;

public sealed class ApiBadRequestResponse
{
    public string Type { get; init; } = string.Empty;
    public string Error { get; init; } = string.Empty;
    public string Detail { get; init; } = string.Empty;
}
