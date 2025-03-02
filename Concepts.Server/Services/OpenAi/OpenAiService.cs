
using Microsoft.Extensions.Options;

namespace Concepts.Server.Services.OpenAi;

public class OpenAiService : IOpenAiService
{
    private readonly OpenAiOptions _options;

    public OpenAiService(IOptions<OpenAiOptions> options)
    {
        _options = options.Value;
    }

    public async Task RequestQueryAsync(string topic) => Console.WriteLine(_options.ApiKey);
}