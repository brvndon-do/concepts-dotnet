
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

    public async Task RequestQueryAsync(string topic)
    {
        List<Message> messages = new List<Message>
        {
            new Message(Role.User, "Can you say 'Hello, world!' in Korean?"),
        };

        ChatRequest request = new ChatRequest(messages);
        ChatResponse response = await _openAiClient.ChatEndpoint.GetCompletionAsync(request);
        Choice choice = response.FirstChoice;

        Console.WriteLine($"***[{choice.Index}] {choice.Message.Role}: {choice.Message} | Finish Reason: {choice.FinishReason} ***");
    }
}