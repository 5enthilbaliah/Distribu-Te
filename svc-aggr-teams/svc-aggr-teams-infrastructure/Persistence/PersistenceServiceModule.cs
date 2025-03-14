namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence;

using Application;
using Domain.Entities;
using Domain.Settings;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class PersistenceServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var dbSettings = new DistribuTeDbSettings();
        configuration.GetSection(nameof(DistribuTeDbSettings)).Bind(dbSettings);
        
        services.AddDbContext<TeamDatabaseContext>(opt => 
            opt.UseSqlServer(dbSettings.ConnectionString, mssqlOpt =>
            {
                mssqlOpt.CommandTimeout(dbSettings.TimeoutInSeconds);
            }));
        
        services.AddScoped<ITeamsReader<AssociateAggregate, AssociateId>, AssociateTeamsRepository>();
        services.AddScoped<ITeamsReader<SquadAssociateAggregate, SquadAssociateId>, SquadAssociateTeamsRepository>();
        services.AddScoped<ITeamsReader<SquadAggregate, SquadId>, SquadTeamsRepository>();
    }
}