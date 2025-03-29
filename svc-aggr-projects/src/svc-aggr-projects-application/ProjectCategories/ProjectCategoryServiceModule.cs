namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using Framework.ModuleZ.Implementations;
using Mappers;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Validations;

public class ProjectCategoryServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId>, ProjectCategoryEntityLinqMapper>();
        
        services.AddScoped<IPipelineBehavior<YieldProjectCategoriesQuery, ErrorOr<IList<ProjectCategoryModel>>>,
            YieldProjectCategoriesQueryValidationBehavior>();
        services.AddScoped<IPipelineBehavior<PickProjectCategoryQuery, ErrorOr<ProjectCategoryModel>>, 
            PickProjectCategoryQueryValidationBehavior>();
    }
}