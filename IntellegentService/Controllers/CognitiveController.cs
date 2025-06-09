using IntellegenceService.Models;
using Microsoft.AspNetCore.Mvc;
using WTI.InsightDaily.UX.DispatcherNS.Interfaces;
using WTI.InsightDaily.WebApi.HttpContextExtensions;

namespace GptApi.Controllers;

[Route("[controller]/api/v1")]
[ApiController]
public class CognitiveController : Controller
{
    private readonly IHttpRequestContext httpRequestContext;

    private readonly IDispatcher dispatcher = null!;

    private readonly ILogger<CognitiveController> logger;

    public CognitiveController(IHttpRequestContext httpRequestContext, IDispatcher dispatcher, ILogger<CognitiveController> logger)
    {
        this.httpRequestContext = httpRequestContext;

        this.dispatcher = dispatcher;

        this.logger = logger;
    }

    [HttpPost]
    [Route("query")]
    public async Task<IActionResult> Index([FromBody]QueryRequest request)
    {
        var tenantUId = httpRequestContext.GetTenantId();
        if (tenantUId == Guid.Empty)
        {
            return Unauthorized();
        }

        if (request == null)
        {
            return BadRequest("Required parameters are Invalid.");
        }

        var queryResponse = await dispatcher.GenerateContentAsync(request);
        if (queryResponse?.ReturnCode == 200)
        {
            return Ok(queryResponse);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected failure occurred. Please contact support.");
    }
}
