namespace DistribuTe.Aggregates.Projects.Application.SquadProjects;

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

public class SquadProjectServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<EntityLinqMapper<SquadProjectAggregate, SquadProjectId>, SquadProjectEntityLinqMapper>();
        
        services.AddScoped<IPipelineBehavior<YieldSquadProjectsQuery, ErrorOr<IList<SquadProjectModel>>>,
            YieldSquadProjectsQueryValidationBehavior>();
        services.AddScoped<IPipelineBehavior<PickSquadProjectQuery, ErrorOr<SquadProjectModel>>, 
            PickSquadProjectQueryValidationBehavior>();
    }
}