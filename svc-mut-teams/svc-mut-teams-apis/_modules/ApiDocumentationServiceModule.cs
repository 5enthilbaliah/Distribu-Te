namespace DistribuTe.Mutators.Teams.Apis.Modules;

using System.Diagnostics.CodeAnalysis;
using Framework.ModuleZ.Implementations;
using Helpers.Swagger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Settings;

public class ApiDocumentationServiceModule : DependencyServiceModule
{
    [ExcludeFromCodeCoverage]
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        var svcSettings = new ServiceSettings();
        configuration.GetSection(nameof(ServiceSettings)).Bind(svcSettings);
        
        services.AddSwaggerGen(c =>
        {
            c.ConfigureSwaggerForOauth();
            c.OperationFilter<CorrelationIdOperationFilter>();
            c.OperationFilter<PublicOperationFilter>();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = $"DistribuTe mutator api for teams - {environment.EnvironmentName} - {svcSettings.Version}",
                Version = "v1",
            });
            c.DescribeAllParametersInCamelCase();

            const string pattern = "DistribuTe.Mutators.Teams.*.xml";
            foreach (var file in Directory.GetFiles(AppContext.BaseDirectory, pattern))
            {
                c.IncludeXmlComments(file, includeControllerXmlComments: true);
            }
        });
    }
}