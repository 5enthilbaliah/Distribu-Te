namespace DistribuTe.Aggregates.Projects.Infrastructure;

using Framework.DomainEssentials.Settings;
using Framework.InfrastructureEssentials.Telemetry;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;

public class InfrastructureServiceModule : DependencyServiceModule
{
    public InfrastructureServiceModule()
    {
        PrependModule<PersistenceServiceModule>();
        AppendModule<TelemetryServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var telemetrySettings = new TelemetrySettings();
        configuration.GetSection(nameof(TelemetrySettings)).Bind(telemetrySettings);
        
        services.AddHealthChecks()
            .AddDbContextCheck<ProjectSchemaDatabaseContext>(name: "sql_server", tags: ["db"]);
    }
}