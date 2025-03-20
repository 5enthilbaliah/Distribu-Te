namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

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

public class SquadAssociateServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId>, SquadAssociateEntityLinqMapper>();
        
        services.AddScoped<IPipelineBehavior<YieldSquadAssociatesQuery, ErrorOr<IList<SquadAssociateModel>>>,
            YieldSquadAssociatesQueryValidationBehavior>();
        services.AddScoped<IPipelineBehavior<PickSquadAssociateQuery, ErrorOr<SquadAssociateModel>>, 
            PickSquadAssociateQueryValidationBehavior>();
    }
}