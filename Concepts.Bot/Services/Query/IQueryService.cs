using Concepts.Bot.Models;

namespace Concepts.Bot.Services.Query;

public interface IQueryService
{
    Task<ConceptDto> GetConceptAsync(string topic);
}