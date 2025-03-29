using Concepts.Server.Models;
using Concepts.Server.Services.OpenAi;
using Microsoft.AspNetCore.Mvc;

namespace Concepts.Server.Controllers;

[ApiController, Route("api/query")]
public class QueryController(
    IOpenAiService openAiService
): ControllerBase
{
    IOpenAiService _openAiService = openAiService;

    [HttpGet]
    public async Task<ActionResult<ConceptDto>> GetRandomConceptAsync([FromQuery] string topic) => Ok(await _openAiService.RequestConceptAsync(topic));
}