namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence;

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
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var dbSettings = new DistribuTeDbSettings();
        configuration.GetSection(nameof(DistribuTeDbSettings)).Bind(dbSettings);
        
        services.AddDbContext<ProjectSchemaDatabaseContext>(opt => 
            opt.UseSqlServer(dbSettings.ConnectionString, mssqlOpt =>
            {
                mssqlOpt.CommandTimeout(dbSettings.TimeoutInSeconds);
            }));
        
        services.AddScoped<IAggregateReader<ProjectAggregate, ProjectId>, ProjectAggregateRepository>();
        services.AddScoped<IAggregateReader<ProjectCategoryAggregate, ProjectCategoryId>, ProjectCategoryAggregateRepository>();
        services.AddScoped<IAggregateReader<SquadProjectAggregate, SquadProjectId>, SquadProjectAggregateRepository>();
    }
}