namespace Concepts.Bot.Services.Query;

public class QueryOptions
{
    public required string BaseUri { get; set; }
    public required string HeaderKeyName { get; set; }
    public required string SecretKey { get; set; }
}