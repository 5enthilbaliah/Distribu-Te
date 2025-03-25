namespace DistribuTe.Mutators.Projects.Infrastructure;

using Application;
using Domain.Settings;
using Framework.AppEssentials;
using Framework.InfrastructureEssentials;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Persistence;
using Refit;
using SiblingResources;

public class InfrastructureServiceModule : DependencyServiceModule
{
    public InfrastructureServiceModule()
    {
        PrependModule<PersistenceServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddMemoryCache();
        
        var apiSettings = new DistribuTeApiSettings();
        configuration.GetSection(nameof(DistribuTeApiSettings)).Bind(apiSettings);

        services.AddRefitClient<ITeamsAggregateApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiSettings.TeamsAggregateApiBaseUrl));
        services.AddScoped<ITeamsAggregateApiReader, TeamsAggregateApiReader>();

        services.AddHealthChecks()
            .AddDbContextCheck<ProjectSchemaDatabaseContext>(name: "sql_server", tags: ["db"])
            .AddCheck<TeamsAggregateApiHealthCheck>("aggr_teams_api", HealthStatus.Unhealthy, 
                tags: ["api", "aggregate"]);
    }
}