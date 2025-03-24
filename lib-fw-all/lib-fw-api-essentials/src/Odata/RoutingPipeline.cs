namespace DistribuTe.Framework.ApiEssentials.Odata;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ModuleZ;

public class RoutingPipeline : IMiddlewarePipeline
{
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.UseRouting();
    }
}