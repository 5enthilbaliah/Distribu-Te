namespace DistribuTe.Mutators.Projects.Application;

using System.Reflection;
using Behaviors;
using FluentValidation;
using Framework.ModuleZ.Implementations;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectCategories.Validations;
using Projects.Validations;
using Shared;

public class ApplicationServiceModule : DependencyServiceModule
{
    public ApplicationServiceModule()
    {
        AppendModule<ProjectValidationServiceModule>();
        AppendModule<ProjectCategoryValidationServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<IRequestContext>());
        
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UserTrackBehavior<,>));
        
        var mapsterConfig = TypeAdapterConfig.GlobalSettings;
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetAssembly(typeof(ApplicationServiceModule))!);
        services.AddSingleton(mapsterConfig);
        services.AddMapster();
        
        services.AddValidatorsFromAssembly(typeof(ApplicationServiceModule).Assembly);
    }
}