namespace DistribuTe.Framework.ApiEssentials.Cors;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ModuleZ;

[ExcludeFromCodeCoverage]
public class CorsPipeline : IMiddlewarePipeline
{
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.UseCors("AllowAll");
    }
}