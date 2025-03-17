namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Domain.Entities;
using Framework.AppEssentials.Implementations;
using Framework.ModuleZ.Implementations;
using Mappers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class SquadServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        services.AddScoped<LinqQueryFilterMapper<SquadAggregate, SquadId>, SquadLinqQueryFilterMapper>();
    }
}