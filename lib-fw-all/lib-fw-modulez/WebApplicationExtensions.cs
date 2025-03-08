namespace DistribuTe.Framework.ModuleZ;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public static class WebApplicationExtensions
{
    public static WebApplication Pipe<TPipeline>(this WebApplication app,
        IWebHostEnvironment environment, IConfiguration configuration)
        where TPipeline : IMiddlewarePipeline, new()
    {
        var pipeline = new TPipeline();
        pipeline.Setup(app, environment, configuration);
        return app;
    }
}