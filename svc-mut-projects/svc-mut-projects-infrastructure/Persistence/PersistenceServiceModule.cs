namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence;

using Domain.Entities;
using Domain.Settings;
using Framework.AppEssentials;
using Framework.InfrastructureEssentials.Persistence;
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
        
        services.AddDbContext<ProjectSchemaDatabaseContext>(opt => 
            opt.UseSqlServer(dbSettings.ConnectionString, mssqlOpt =>
            {
                mssqlOpt.CommandTimeout(dbSettings.TimeoutInSeconds);
            }));

        // TODO:: find a way to register the same repository for both mutators and readers
        services.AddScoped<IEntityMutator<Project, ProjectId>, ProjectEntityRepository>();
        services.AddScoped<IEntityMutator<ProjectCategory, ProjectCategoryId>, ProjectCategoryEntityRepository>();
        services.AddScoped<IEntityMutator<SquadProject, SquadProjectId>, SquadProjectEntityRepository>();
        
        services.AddScoped<IEntityReader<Project, ProjectId>, ProjectEntityRepository>();
        services.AddScoped<IEntityReader<ProjectCategory, ProjectCategoryId>, ProjectCategoryEntityRepository>();
        services.AddScoped<IEntityReader<SquadProject, SquadProjectId>, SquadProjectEntityRepository>();

        services.AddScoped(typeof(IExistingEntityMarker<,>), typeof(ExistingEntityMarker<,>));
        
        services.AddScoped<IUnitOfWork>(sp =>
        {
            var dbContext = sp.GetRequiredService<ProjectSchemaDatabaseContext>();
            var dateTimeProvider = sp.GetRequiredService<IDateTimeProvider>();
            return new UnitOfWork(dbContext, dateTimeProvider);
        });
    }
}