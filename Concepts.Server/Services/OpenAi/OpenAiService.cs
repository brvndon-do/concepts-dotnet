using System.ClientModel;
using Concepts.Server.Models;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;

namespace Concepts.Server.Services.OpenAi;

public class OpenAiService : IOpenAiService
{
    private readonly OpenAiOptions _options;
    private readonly ChatClient _chatClient;

    public OpenAiService(IOptions<OpenAiOptions> options)
    {
        _options = options.Value;

        OpenAIClient openAiClient = new OpenAIClient(_options.ApiKey);
        _chatClient = openAiClient.GetChatClient(_options.Model);
    }

    public async Task<ConceptDto> RequestConceptAsync(string topic)
    {
        List<ChatMessage> messages = new List<ChatMessage>
        {
            ChatMessage.CreateSystemMessage(Prompts.SYSTEM_PROMPT),
            ChatMessage.CreateSystemMessage(Prompts.SYSTEM_PROMPT_FOLLOWUP),
            ChatMessage.CreateUserMessage(Prompts.FormatUserPrompt(topic))
        };

        ChatCompletion completion = await _chatClient.CompleteChatAsync(messages);

        if (completion.Content.Count < 0)
            throw new Exception("Unexpected response with no content");

        return new ConceptDto
        {
            Topic = topic,
            Message = completion.Content.First().Text,
            Timestamp = completion.CreatedAt,
        };
    }

    public async IAsyncEnumerable<string> RequestConceptStreamAsync(string topic)
    {
        List<ChatMessage> messages = new List<ChatMessage>
        {
            ChatMessage.CreateSystemMessage(Prompts.SYSTEM_PROMPT),
            ChatMessage.CreateSystemMessage(Prompts.SYSTEM_PROMPT_FOLLOWUP),
            ChatMessage.CreateUserMessage(Prompts.FormatUserPrompt(topic))
        };

        AsyncCollectionResult<StreamingChatCompletionUpdate> completionUpdates = _chatClient.CompleteChatStreamingAsync(messages);

        await foreach (StreamingChatCompletionUpdate update in completionUpdates)
        {
            if (update.ContentUpdate.Count > 0)
            {
                yield return update.ContentUpdate.First().Text;
            }
        }
    }
}