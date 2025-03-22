namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence;

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
        
        services.AddDbContext<TeamSchemaDatabaseContext>(opt => 
            opt.UseSqlServer(dbSettings.ConnectionString, mssqlOpt =>
            {
                mssqlOpt.CommandTimeout(dbSettings.TimeoutInSeconds);
            }));

        // TODO:: find a way to register the same repository for both mutators and readers
        services.AddScoped<IEntityMutator<Associate, AssociateId>, AssociateEntityRepository>();
        services.AddScoped<IEntityMutator<SquadAssociate, SquadAssociateId>, SquadAssociateEntityRepository>();
        services.AddScoped<IEntityMutator<Squad, SquadId>, SquadEntityRepository>();
        
        services.AddScoped<IEntityReader<Associate, AssociateId>, AssociateEntityRepository>();
        services.AddScoped<IEntityReader<SquadAssociate, SquadAssociateId>, SquadAssociateEntityRepository>();
        services.AddScoped<IEntityReader<Squad, SquadId>, SquadEntityRepository>();

        services.AddScoped(typeof(IExistingEntityMarker<,>), typeof(ExistingEntityMarker<,>));
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}