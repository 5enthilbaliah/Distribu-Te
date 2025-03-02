// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Pipelines;

using Framework.ModuleZ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public class CorsPipeline : IMiddlewarePipeline
{
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.UseCors("AllowAll");
    }
}