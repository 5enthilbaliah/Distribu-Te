namespace DistribuTe.Aggregates.Teams.Application.Squads;

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

public class SquadServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<EntityLinqMapper<SquadAggregate, SquadId>, SquadEntityLinqMapper>();
        
        services.AddScoped<IPipelineBehavior<YieldSquadsQuery, ErrorOr<IList<SquadModel>>>,
            YieldSquadsQueryValidationBehavior>();
        services.AddScoped<IPipelineBehavior<PickSquadQuery, ErrorOr<SquadModel>>, 
            PickSquadQueryValidationBehavior>();
    }
}