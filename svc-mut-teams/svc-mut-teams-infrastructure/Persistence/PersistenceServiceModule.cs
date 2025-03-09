namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Application;
using Application.Shared;
using Domain.Entities;
using Domain.Settings;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class PersistenceServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, IConfiguration configuration)
    {
        var dbSettings = new DistribuTeDbSettings();
        configuration.GetSection(nameof(DistribuTeDbSettings)).Bind(dbSettings);
        
        services.AddDbContext<TeamDatabaseContext>(opt => 
            opt.UseSqlServer(dbSettings.ConnectionString, mssqlOpt =>
            {
                mssqlOpt.CommandTimeout(dbSettings.TimeoutInSeconds);
            }));

        services.AddScoped<ITeamsMutator<Associate, AssociateId>, AssociateTeamsMutator>();
        services.AddScoped<ITeamsMutator<SquadAssociate, SquadAssociateId>, SquadAssociateTeamsMutator>();
        services.AddScoped<ITeamsMutator<Squad, SquadId>, SquadTeamsMutator>();
        
        services.AddScoped<ITeamsReader<Associate, AssociateId>, AssociateTeamsReader>();
        services.AddScoped<ITeamsReader<SquadAssociate, SquadAssociateId>, SquadAssociateTeamsReader>();
        services.AddScoped<ITeamsReader<Squad, SquadId>, SquadTeamsReader>();

        services.AddScoped(typeof(IExistingEntityMarker<,>), typeof(ExistingEntityMarker<,>));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}