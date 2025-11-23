using System.Net.Http.Json;
using Concepts.Bot.Models;
using Microsoft.Extensions.Options;

namespace Concepts.Bot.Services.Query;

public class QueryService(HttpClient httpClient, IOptions<QueryOptions> options) : IQueryService
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly QueryOptions _options = options.Value;

    public async Task<ConceptDto> GetConceptAsync(string topic)
    {
        HttpRequestMessage requestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            $"{_options.BaseUri}/api/query?topic={Uri.EscapeDataString(topic)}"
        );

        HttpResponseMessage responseMessage = await _httpClient.SendAsync(requestMessage);

        if (!responseMessage.IsSuccessStatusCode)
            throw new Exception("Error retrieving a response from server");

        return await responseMessage.Content.ReadFromJsonAsync<ConceptDto>() ?? throw new Exception("Error parsing response");
    }
}