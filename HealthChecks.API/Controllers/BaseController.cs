using Microsoft.AspNetCore.Mvc;
using HealthChecks.API.Controllers.Constants;
using HealthChecks.API.Controllers.Responses;

namespace HealthChecks.API.Controllers;

public class BaseController : ControllerBase
{
    protected IActionResult ApiBadRequest(ApiErrorType type, string businessError, string? exceptionMessage)
    {
        return StatusCode(StatusCodes.Status400BadRequest, new ApiBadRequestResponse
        {
            Type = type.ToString(),
            Error = businessError,
            Detail = exceptionMessage ?? string.Empty
        });
    }
}
