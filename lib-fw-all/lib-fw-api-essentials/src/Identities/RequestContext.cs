// ReSharper disable InconsistentNaming
namespace DistribuTe.Framework.ApiEssentials.Identities;

using System.Security.Claims;
using AppEssentials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Swagger;

public class RequestContext(IHttpContextAccessor accessor) : IRequestContext
{
    private readonly IHttpContextAccessor _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
    private readonly Guid _defaultCorrelationId = Guid.NewGuid();
    
    public const string DEFAULT_USER_NAME = "Anonymous";
    public const string DEFAULT_TOKEN = "Anonymous";
    public const string HEADER_CORRELATION_ID = CorrelationIdOperationFilter.HEADER_CORRELATION_ID;
    public const string HEADER_AUTHORIZATION = "Authorization";

    public HttpContext Current => _accessor.HttpContext!;
    public ClaimsPrincipal? User => Current?.User;
    public IFeatureCollection? Features => Current?.Features;
    
    public string CorrelationId => (Current?.Request?.Headers?.TryGetValue(HEADER_CORRELATION_ID, out var correlationId) ?? false 
        ? correlationId! : _defaultCorrelationId.ToString());
    public string Token => (Current?.Request?.Headers?.TryGetValue(HEADER_AUTHORIZATION, out var authorization) ?? false 
        ? authorization!.First()!.Split([' '], StringSplitOptions.RemoveEmptyEntries)[1] : DEFAULT_TOKEN);

    public string UserIdentity => User?.Identity?.Name ?? DEFAULT_USER_NAME;
    public string UserEmail => User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
    
    
    public string HttpMethod => Current.Request.Method;

    public TFeature? GetFeature<TFeature>()
    {
        return Features == null ? default : Features.Get<TFeature>();
    }
    
    public void Set<TFeature>(TFeature instance)
    {
        if (Features == null) return;
        Features[typeof(TFeature)] = instance;
    }
}