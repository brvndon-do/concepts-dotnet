using System.Net.Http.Json;
using Concepts.Bot.Models;
using Microsoft.Extensions.Options;

namespace Concepts.Bot.Services.Query;

public class QueryService(IHttpClientFactory httpClientFactory, IOptions<QueryOptions> options) : IQueryService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly QueryOptions _options = options.Value;

    public async Task<ConceptDto> GetConceptAsync(string topic)
    {
        using HttpClient httpClient = _httpClientFactory.CreateClient();

        HttpRequestMessage requestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_options.BaseUri}/api/query?topic={topic}"
        );

        ConceptDto? dto = await httpClient.GetFromJsonAsync<ConceptDto>(
            $"{_options.BaseUri}/api/query?topic={topic}"
        );

        return dto ?? throw new Exception("Error retrieving a response from server");
    }
}