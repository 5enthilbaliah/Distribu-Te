namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

using Application;
using Domain.Entities;
using Domain.Settings;
using Framework.AppEssentials;
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

        // TODO:: find a way to register the same repository for both mutators and readers
        services.AddScoped<ITeamsMutator<Associate, AssociateId>, AssociateTeamsRepository>();
        services.AddScoped<ITeamsMutator<SquadAssociate, SquadAssociateId>, SquadAssociateTeamsRepository>();
        services.AddScoped<ITeamsMutator<Squad, SquadId>, SquadTeamsRepository>();
        
        services.AddScoped<ITeamsReader<Associate, AssociateId>, AssociateTeamsRepository>();
        services.AddScoped<ITeamsReader<SquadAssociate, SquadAssociateId>, SquadAssociateTeamsRepository>();
        services.AddScoped<ITeamsReader<Squad, SquadId>, SquadTeamsRepository>();

        services.AddScoped(typeof(IExistingEntityMarker<,>), typeof(ExistingEntityMarker<,>));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}