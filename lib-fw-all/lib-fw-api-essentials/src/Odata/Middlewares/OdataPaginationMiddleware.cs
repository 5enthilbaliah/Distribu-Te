namespace DistribuTe.Framework.ApiEssentials.Odata.Middlewares;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.Extensions.DependencyInjection;

public class OdataPaginationMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
{
    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));
    private readonly IServiceProvider _serviceProvider = serviceProvider ?? 
                                                         throw new ArgumentNullException(nameof(serviceProvider));

    public async Task InvokeAsync(HttpContext httpContext)
    {
        // Capture the original response body stream
        var responseStream = httpContext.Response.Body;

        // Replace it with our own, so that we can read it
        using var bodyStream = new MemoryStream();
        httpContext.Response.Body = bodyStream;

        try
        {
            await _next(httpContext);
        }
        catch (Exception)
        {
            // Handle exception on odata parsing - set back the response stream to body
            httpContext.Response.Body = responseStream;
            throw;
        }
        
        httpContext.Response.Body = responseStream;
        bodyStream.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(bodyStream).ReadToEndAsync();
        
        if (httpContext.Response.StatusCode == StatusCodes.Status200OK)
        {
            var jsonFirstCharMemory = new ReadOnlyMemory<char>(['[']);
            var isArray = responseBody.TrimStart() switch
            {
                { Length: > 0 } str => str[..1].AsSpan()
                    .Equals(jsonFirstCharMemory.Span,
                        StringComparison.InvariantCultureIgnoreCase),
                _ => false
            };

            if (isArray)
            {
                var pageId = httpContext.Request.Path.Value!.Split(['/'])[^1].ToLower();
                
                using var scope = _serviceProvider.CreateScope();
                var paginator = scope.ServiceProvider.GetRequiredKeyedService<IOdataPaginator>(pageId);
                var count = await paginator.CountAsync(CancellationToken.None);
                responseBody = $"{{ \"count\": {count}, \"results\": {responseBody} }}";
            }
        }
        
        await httpContext.Response.WriteAsync(responseBody);
    }
}