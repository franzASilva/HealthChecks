using HealthChecks.API.Controllers.Constants;
using HealthChecks.API.Controllers.Responses;
using HealthChecks.Domain.Models;
using HealthChecks.Domain.Records;
using HealthChecks.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthChecks.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DummyController() : BaseController
{
    /// <summary>
    /// Get all Dummies values
    /// </summary>
    /// <param name="dummyService"></param>
    /// <param name="id"></param>
    /// <param name="ct"></param>
    /// <returns>Only one dummy</returns>
    /// <response code="200">Returns a dummy</response>
    /// <response code="404">If the item is null or zero</response>
    [HttpGet("{id}", Name = "GetDummyById")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<DummyRecord>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(IDummyService dummyService, int id, CancellationToken ct)
    {
        var ret = await dummyService.GetDummyAsync(id, ct);

        if (ret.Success)
        {
            return Ok(ret.ReturnValue);
        }

        return NotFound(ret.BusinessError);
    }

    /// <summary>
    /// Get all Dummies values
    /// </summary>
    /// <param name="dummyService"></param>
    /// <param name="ct"></param>
    /// <returns>List of dummies</returns>
    /// <response code="200">Returns a list of dummies</response>
    /// <response code="404">If the item is null or zero</response>
    [HttpGet(Name = "GetDummiesValues")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(List<DummyRecord>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(IDummyService dummyService, CancellationToken ct)
    {
        var ret = await dummyService.GetDummiesValuesAsync(ct);

        if (ret.Success)
        {
            return Ok(ret.ReturnValue);
        }

        return NotFound(ret.BusinessError);
    }

    /// <summary>
    /// Save a dummy
    /// </summary>
    /// <param name="dummyService"></param>
    /// <param name="dummy"></param>
    /// <param name="ct"></param>
    /// <returns>Saved dummy</returns>
    /// <response code="200">Dummy was saved</response>
    /// <response code="400">Dummy wasn't saved</response>
    [HttpPost(Name = "PostNewDummy")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(DummyModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiBadRequestResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(IDummyService dummyService, DummyModel dummy, CancellationToken ct)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var ret = await dummyService.SaveDummyAsync(dummy, ct);

        if (ret.Success)
        {
            return Ok(ret.ReturnValue);
        }

        return ApiBadRequest(ApiErrorType.BusinessError, ret.BusinessError, ret.Error?.Message);
    }
}
