namespace DistribuTe.Mutators.Teams.Apis;

using Framework.ApiEssentials.Auth;
using Framework.ApiEssentials.Swagger;
using Framework.ModuleZ;
using Framework.OData.ErrorHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Pipelines;

public static class ApiSetup
{
    public static WebApplication Setup(this WebApplication app,
        IWebHostEnvironment environment, IConfiguration configuration)
    {
        return app.Pipe<HttpsRedirectionPipeline>(environment, configuration)
            .Pipe<ApiDocumentationPipeline>(environment, configuration)
            .Pipe<CorsPipeline>(environment, configuration)
            .Pipe<AuthenticationPipeline>(environment, configuration)
            //TOMARE:: custom error handling pipeline here - order matters
            .Pipe<ErrorHandlePipeline>(environment, configuration)
            .Pipe<MvcPipeline>(environment, configuration);
    }
}