namespace DistribuTe.Mutators.Teams.Apis;

using Framework.ApiEssentials;
using Framework.ApiEssentials.Auth;
using Framework.ApiEssentials.Cors;
using Framework.ApiEssentials.Identities;
using Framework.ApiEssentials.Swagger;
using Framework.ApiEssentials.Versioning;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Odata;

public class ApiServiceModule : DependencyServiceModule
{
    public ApiServiceModule()
    {
        PrependModule<IdentityServiceModule>();
        PrependModule<AuthenticationServiceModule>();
        PrependModule<ApiVersionServiceModule>();
        PrependModule<CorsServiceModule>();
        PrependModule<ControllerServiceModule>();
        PrependModule<ApiDocumentationServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.Configure<ServiceSettings>(configuration.GetSection(nameof(ServiceSettings)));
        services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));
        services.AddHealthChecks();
    }
}