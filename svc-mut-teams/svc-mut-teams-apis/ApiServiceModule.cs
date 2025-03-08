namespace DistribuTe.Mutators.Teams.Apis;

using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules;
using Settings;

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