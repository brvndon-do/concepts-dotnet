
using Concepts.Server.Models;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;

namespace Concepts.Server.Services.OpenAi;

public class OpenAiService : IOpenAiService
{
    private readonly OpenAiOptions _options;
    private readonly OpenAIClient _openAiClient;

    public OpenAiService(IOptions<OpenAiOptions> options)
    {
        _options = options.Value;
        _openAiClient = new OpenAIClient(new OpenAIAuthentication(_options.ApiKey));

    }

    public async Task<ConceptDto> RequestConceptAsync(string topic)
    {
        List<Message> messages = new List<Message>
        {
            new Message(Role.System, Prompts.SystemPrompt),
            new Message(Role.User, topic),
        };

        ChatRequest request = new ChatRequest(
            messages: messages,
            model: "gpt-4o-mini"
        );
        ChatResponse response = await _openAiClient.ChatEndpoint.GetCompletionAsync(request);
        Choice choice = response.FirstChoice;

        return new ConceptDto
        {
            Topic = topic,
            Message = choice.Message,
            Timestamp = DateTimeOffset.FromUnixTimeSeconds(response.CreatedAtUnixTimeSeconds),
        };
    }
}