namespace DistribuTe.Aggregates.Projects.Application.Projects;

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

public class ProjectServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<EntityLinqMapper<ProjectAggregate, ProjectId>, ProjectEntityLinqMapper>();
        
        services.AddScoped<IPipelineBehavior<YieldProjectsQuery, ErrorOr<IList<ProjectModel>>>,
            YieldProjectsQueryValidationBehavior>();
        services.AddScoped<IPipelineBehavior<PickProjectQuery, ErrorOr<ProjectModel>>, 
            PickProjectQueryValidationBehavior>();
    }
}