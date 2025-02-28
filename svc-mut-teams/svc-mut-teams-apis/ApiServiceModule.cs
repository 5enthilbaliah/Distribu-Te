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
        AddModule<IdentityServiceModule>();
        AddModule<AuthenticationServiceModule>();
        AddModule<ApiVersionServiceModule>();
        AddModule<CorsServiceModule>();
        AddModule<ControllerServiceModule>();
        AddModule<ApiDocumentationServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.Configure<ServiceSettings>(configuration.GetSection(nameof(ServiceSettings)));
        services.Configure<SwaggerSettings>(configuration.GetSection(nameof(SwaggerSettings)));
        services.AddHealthChecks();
    }
}