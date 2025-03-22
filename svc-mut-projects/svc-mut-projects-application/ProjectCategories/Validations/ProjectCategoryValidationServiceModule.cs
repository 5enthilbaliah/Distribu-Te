namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.Validations;

using DataContracts;
using ErrorOr;
using Framework.ModuleZ.Implementations;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class ProjectCategoryValidationServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddScoped<IPipelineBehavior<SpawnProjectCategoryCommand, ErrorOr<ProjectCategoryResponse>>,
            SpawnProjectCategoryCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<CommitProjectCategoryCommand, ErrorOr<ProjectCategoryResponse>>,
            CommitProjectCategoryCommandValidationBehavior>();
        services.AddScoped<IPipelineBehavior<TrashProjectCategoryCommand, ErrorOr<bool>>,
            TrashProjectCategoryCommandValidationBehavior>();
    }
}