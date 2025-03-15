namespace DistribuTe.Framework.ApiEssentials.Auth;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ModuleZ;

[ExcludeFromCodeCoverage]
public class HttpsRedirectionPipeline : IMiddlewarePipeline
{
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.UseHttpsRedirection();
    }
}