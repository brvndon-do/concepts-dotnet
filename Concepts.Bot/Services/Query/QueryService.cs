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

        HttpResponseMessage responseMessage = await httpClient.SendAsync(requestMessage);

        if (!responseMessage.IsSuccessStatusCode)
            throw new Exception("Error retrieving a response from server");

        ConceptDto? dto = await responseMessage.Content.ReadFromJsonAsync<ConceptDto>();

        return dto ?? throw new Exception("Error parsing response");
    }
}