namespace DistribuTe.Framework.ApiEssentials.Odata;

using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ModuleZ;

public class MutatorControllerPipeline : IMiddlewarePipeline
{
    [ExcludeFromCodeCoverage]
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.UseRouting();
        app.MapControllers();
    }
}