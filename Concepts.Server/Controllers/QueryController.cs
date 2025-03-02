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
    public ActionResult GetRandomConceptAsync() => throw new NotImplementedException();

    [HttpPost]
    public ActionResult AddTopicAsync([FromBody] string topic) => throw new NotImplementedException();
}