namespace DistribuTe.Aggregates.Projects.Application;

using System.Reflection;
using FluentValidation;
using Framework.ModuleZ.Implementations;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projects;

public class ApplicationServiceModule : DependencyServiceModule
{
    public ApplicationServiceModule ()
    {
        PrependModule<ProjectServiceModule>();
    }
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ApplicationServiceModule>());
        
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(ApplicationServiceModule))!);
        services.AddSingleton(mapsterConfig);
        services.AddMapster();
        
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceModule).Assembly);
    }
}