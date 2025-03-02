namespace Concepts.Server.Services.OpenAi;

public interface IOpenAiService
{
    Task RequestQueryAsync(string topic);
}