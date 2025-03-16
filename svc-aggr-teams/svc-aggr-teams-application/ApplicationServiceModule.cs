namespace DistribuTe.Aggregates.Teams.Application;

using System.Reflection;
using Associates;
using FluentValidation;
using Framework.ModuleZ.Implementations;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using SquadAssociates;
using Squads;

public class ApplicationServiceModule : DependencyServiceModule
{
    public ApplicationServiceModule ()
    {
        PrependModule<AssociateServiceModule>();
        PrependModule<SquadAssociateServiceModule>();
        PrependModule<SquadServiceModule>();
    }
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<IRequestContext>());
        
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(ApplicationServiceModule))!);
        services.AddSingleton(mapsterConfig);
        services.AddMapster();
        
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceModule).Assembly);
    }
}