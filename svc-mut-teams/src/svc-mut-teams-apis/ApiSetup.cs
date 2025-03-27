namespace DistribuTe.Mutators.Teams.Apis;

using Framework.ApiEssentials.Auth;
using Framework.ApiEssentials.Cors;
using Framework.ApiEssentials.Odata;
using Framework.ApiEssentials.Odata.ErrorHandling;
using Framework.ApiEssentials.Swagger;
using Framework.ApiEssentials.Telemetry;
using Framework.ModuleZ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

public static class ApiSetup
{
    public static WebApplication Setup(this WebApplication app,
        IWebHostEnvironment environment, IConfiguration configuration)
    {
        // https redirection is not needed since the application will be hosted behind a reverse proxy and managed by an
        // api gateway - you can use it when you are deploying it as stand alone
        return app.Pipe<ApiDocumentationPipeline>(environment, configuration)
            .Pipe<CorsPipeline>(environment, configuration)
            .Pipe<TelemetryPipeline>(environment, configuration)
            .Pipe<RoutingPipeline>(environment, configuration)
            .Pipe<AuthenticationPipeline>(environment, configuration)
            //TOMARE:: custom error handling pipeline here - order matters
            .Pipe<ErrorHandlePipeline>(environment, configuration)
            .Pipe<MutatorControllerPipeline>(environment, configuration);
    }
}