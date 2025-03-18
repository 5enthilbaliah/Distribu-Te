// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Pipelines;

using System.Diagnostics.CodeAnalysis;
using Framework.ApiEssentials.Odata.Middlewares;
using Framework.ModuleZ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public class MvcPipeline : IMiddlewarePipeline
{
    [ExcludeFromCodeCoverage]
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.UseRouting();
        // app.UseMiddleware<OdataPaginationMiddleware>();
        app.MapControllers();
    }
}