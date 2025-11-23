using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Concepts.Server.Utilities.AuthenticatedRequest;

public class AuthenticatedRequestFilter(IConfiguration configuration) : IAsyncActionFilter
{
    private readonly IConfiguration _configuration = configuration;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        IConfigurationSection authenticatedSection = _configuration.GetSection("AuthenticatedRequest");
        string headerValue = authenticatedSection.GetValue<string>("HeaderKeyName") ?? throw new ArgumentNullException();
        string secretKey = authenticatedSection.GetValue<string>("SecretKey") ?? throw new ArgumentNullException();

        HttpRequest request = context.HttpContext.Request;

        if (!request.Headers.TryGetValue(headerValue, out var extractedApiKey) || extractedApiKey != secretKey)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        await next();
    }
}