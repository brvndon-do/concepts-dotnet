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

    [HttpGet("ping")]
    public ActionResult GetPing() => Ok("Pong!");

    [HttpGet]
    public async Task<ActionResult<ConceptDto>> GetRandomConceptAsync()
    {
        ConceptDto dto = await _openAiService.RequestConceptAsync("multithreading");

        return Ok(dto);
    }

    [HttpPost]
    public ActionResult AddTopicAsync([FromBody] string topic) => throw new NotImplementedException();
}