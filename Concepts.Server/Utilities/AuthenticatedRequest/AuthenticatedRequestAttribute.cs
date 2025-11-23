using Microsoft.AspNetCore.Mvc;

namespace Concepts.Server.Utilities.AuthenticatedRequest;

public class AuthenticatedRequestAttribute : TypeFilterAttribute
{
    public AuthenticatedRequestAttribute() : base(typeof(AuthenticatedRequestFilter)) { }
}