// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Pipelines;

using Framework.ModuleZ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Settings;

public class ApiDocumentationPipeline : IMiddlewarePipeline
{
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        var swaggerSettings = configuration.GetSection(nameof(SwaggerSettings)).Get<SwaggerSettings>()!;
        app.UseSwagger(opt =>
        {
            opt.RouteTemplate = swaggerSettings.JsonRoute;
            opt.PreSerializeFilters.Add((swaggerDoc, _) =>
            {
                swaggerDoc.Servers = 
                [
                    new OpenApiServer
                    {
                        Url = swaggerSettings.ServerUrl
                    }
                ];
            });
        });
        app.UseSwaggerUI(opt =>
        {
            opt.SwaggerEndpoint(swaggerSettings.UiEndpoint, swaggerSettings.Description);
        });
    }
}