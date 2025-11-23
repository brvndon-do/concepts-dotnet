using Concepts.Bot.Models;
using Concepts.Bot.Services.Query;
using NetCord;
using NetCord.Rest;
using NetCord.Services.ApplicationCommands;

namespace Concepts.Bot.Services.NetCord;

public class QueryModule(
    IQueryService queryService
) : ApplicationCommandModule<ApplicationCommandContext>
{
    private readonly IQueryService _queryService = queryService;

    [SlashCommand(name: "ping", description: "Pong!")]
    public string GetPong() => $"Pong! Timestamp: {DateTimeOffset.UtcNow}";

    [SlashCommand(name: "concept", description: "Query for a concept")]
    public async Task GetConceptAsync(
        [SlashCommandParameter(Name = "topic", Description = "Topic to learn from")] string topic
    )
    {
        await Context.Interaction.SendResponseAsync(InteractionCallback.DeferredMessage(MessageFlags.Loading));

        ConceptDto conceptDto = await _queryService.GetConceptAsync(topic);

        InteractionMessageProperties message = new InteractionMessageProperties
        {
            Embeds =
            [
                new EmbedProperties
                {
                    Title = $"Topic requested: {topic}",
                    Color = new Color(143, 188, 255),
                    Description = conceptDto.Message,
                    Timestamp = conceptDto.Timestamp
                }
            ]
        };

        await Context.Interaction.SendFollowupMessageAsync(message);
    }
}