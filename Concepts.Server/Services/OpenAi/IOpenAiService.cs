using Concepts.Server.Models;

namespace Concepts.Server.Services.OpenAi;

public interface IOpenAiService
{
    Task<ConceptDto> RequestConceptAsync(string topic);
}