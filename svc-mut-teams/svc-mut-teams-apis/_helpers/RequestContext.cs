// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Helpers;

using System.Security.Claims;
using Application.Shared;
using Helpers.Swagger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

public class RequestContext(IHttpContextAccessor accessor) : IRequestContext
{
    private readonly IHttpContextAccessor _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    private readonly Guid _defaultCorrelationId = Guid.NewGuid();
    
    // ReSharper disable once InconsistentNaming
    public const string DEFAULT_USER_NAME = "Anonymous";
    // ReSharper disable once InconsistentNaming
    public const string HEADER_CORRELATION_ID = CorrelationIdOperationFilter.HEADER_CORRELATION_ID;
    
    public HttpContext Current => _accessor.HttpContext!;
    public ClaimsPrincipal? User => Current?.User;
    public IFeatureCollection? Features => Current?.Features;
    
    public string CorrelationId => (Current?.Request?.Headers?.TryGetValue(HEADER_CORRELATION_ID, out var correlationId) ?? false 
        ? correlationId! : _defaultCorrelationId.ToString());

    public string UserIdentity => User?.Identity?.Name ?? DEFAULT_USER_NAME;
    public string UserEmail => User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    
    
    public string HttpMethod => Current.Request.Method;

    public TFeature? GetFeature<TFeature>()
    {
        return Features == null ? default : Features.Get<TFeature>();
    }
}