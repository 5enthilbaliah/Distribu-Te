namespace DistribuTe.Mutators.Projects.Application;

using System.Reflection;
using FluentValidation;
using Framework.AppEssentials.Behaviors;
using Framework.ModuleZ.Implementations;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectCategories.Validations;
using Projects.Validations;
using SquadProjects.Validations;

public class ApplicationServiceModule : DependencyServiceModule
{
    public ApplicationServiceModule()
    {
        AppendModule<ProjectValidationServiceModule>();
        AppendModule<ProjectCategoryValidationServiceModule>();
        AppendModule<SquadProjectValidationServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<ApplicationServiceModule>());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserTrackBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TokenTrackBehavior<,>));
        
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(ApplicationServiceModule))!);
        services.AddSingleton(mapsterConfig);
        services.AddMapster();
        
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceModule).Assembly);
    }
}