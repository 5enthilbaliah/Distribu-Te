namespace DistribuTe.Mutators.Teams.Application;

using System.Reflection;
using Framework.ModuleZ.Implementations;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;

public class ApplicationServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<IRequestContext>());
        
        // Register global behaviors - else register with application scope folder
        // services.AddTransient(typeof(IPipelineBehavior<,>, typeof(AuthenticationBehavior<,>)));
        
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(ApplicationServiceModule))!);
        services.AddSingleton(mapsterConfig);
        services.AddMapster();
    }
}