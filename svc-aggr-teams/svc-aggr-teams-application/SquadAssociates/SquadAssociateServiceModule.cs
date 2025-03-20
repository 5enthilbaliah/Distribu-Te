namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using Framework.ModuleZ.Implementations;
using Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class SquadAssociateServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId>, SquadAssociateEntityLinqMapper>();
    }
}