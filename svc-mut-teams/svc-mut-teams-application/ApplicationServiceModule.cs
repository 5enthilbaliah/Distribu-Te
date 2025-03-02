namespace DistribuTe.Mutators.Teams.Application;

using System.Reflection;
using Behaviors;
using Framework.ModuleZ.Implementations;
using Mapster;
using MediatR;
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
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserTrackBehavior<,>));
        
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(ApplicationServiceModule))!);
        services.AddSingleton(mapsterConfig);
        services.AddMapster();
    }
}