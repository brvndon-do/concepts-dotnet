using Concepts.Bot.Models;
using Concepts.Bot.Services.Query;
using NetCord.Services.ApplicationCommands;

namespace Concepts.Bot.Services.NetCord;

public class QueryModule(IQueryService queryService) : ApplicationCommandModule<ApplicationCommandContext>
{
    private readonly IQueryService _queryService = queryService;

    [SlashCommand(name: "ping", description: "Pong!")]
    public string GetPong() => $"Pong! Timestamp: {DateTimeOffset.UtcNow}";

    [SlashCommand(name: "concept", description: "Query for a concept")]
    public async Task<string> GetConceptAsync(
        [SlashCommandParameter(Name = "topic", Description = "Topic to learn from")] string topic
    )
    {
        ConceptDto conceptDto = await _queryService.GetConceptAsync(topic);

        return conceptDto.Message;
    }
}