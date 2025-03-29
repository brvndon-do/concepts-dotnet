namespace Concepts.Bot.Models;

public class ConceptDto
{
    public required string Topic { get; set; }
    public required string Message { get; set; }
    public DateTimeOffset Timestamp { get; set; }
}