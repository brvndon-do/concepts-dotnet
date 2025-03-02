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
    public async Task<IActionResult> GetRandomConceptAsync()
    {
        await _openAiService.RequestQueryAsync(string.Empty);

        return Ok();
    }

    [HttpPost]
    public ActionResult AddTopicAsync([FromBody] string topic) => throw new NotImplementedException();
}