using Concepts.Server.Models;
using Concepts.Server.Services.OpenAi;
using Concepts.Server.Utilities.AuthenticatedRequest;
using Microsoft.AspNetCore.Mvc;

namespace Concepts.Server.Controllers;

[AuthenticatedRequest, ApiController, Route("api/query")]
public class QueryController(
    IOpenAiService openAiService
) : ControllerBase
{
    private readonly IOpenAiService _openAiService = openAiService;

    [HttpGet]
    public async Task<ConceptDto> GetConceptAsync([FromQuery] string topic) => await _openAiService.RequestConceptAsync(topic);

    [HttpGet("stream")]
    public async Task StreamConceptAsync([FromQuery] string topic)
    {
        Response.ContentType = "text/event-stream";
        await foreach (string token in _openAiService.RequestConceptStreamAsync(topic))
        {
            await Response.WriteAsync(token);
            await Response.Body.FlushAsync();
        }
    }
}