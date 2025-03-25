namespace DistribuTe.Aggregates.Teams.Apis;

using Framework.ApiEssentials.Auth;
using Framework.ApiEssentials.Cors;
using Framework.ApiEssentials.Health;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.ErrorHandling;
using Framework.ApiEssentials.Swagger;
using Framework.ModuleZ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public static class ApiSetup
{
    public static WebApplication Setup(this WebApplication app,
        IWebHostEnvironment environment, IConfiguration configuration)
    {
        return app.Pipe<HttpsRedirectionPipeline>(environment, configuration)
            .Pipe<ApiDocumentationPipeline>(environment, configuration)
            .Pipe<CorsPipeline>(environment, configuration)
            .Pipe<HealthCheckPipeline>(environment, configuration)
            .Pipe<RoutingPipeline>(environment, configuration)
            .Pipe<AuthenticationPipeline>(environment, configuration)
            //TOMARE:: custom error handling pipeline here - order matters
            .Pipe<ErrorHandlePipeline>(environment, configuration)
            .Pipe<AggregatorControllerPipeline>(environment, configuration);
    }
}