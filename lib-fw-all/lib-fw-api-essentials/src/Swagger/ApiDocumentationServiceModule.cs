namespace DistribuTe.Framework.ApiEssentials.Swagger;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleZ.Implementations;

public class ApiDocumentationServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var svcSettings = new ServiceSettings();
        configuration.GetSection(nameof(ServiceSettings)).Bind(svcSettings);
        
        services.ConfigureSwagger(environment, configuration, title: svcSettings.Name,
            docPathPattern: svcSettings.DocumentationPathPattern);
    }
}