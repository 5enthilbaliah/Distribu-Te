namespace DistribuTe.Mutators.Projects.Infrastructure;

using Application;
using Domain.Settings;
using Framework.AppEssentials;
using Framework.DomainEssentials.Settings;
using Framework.InfrastructureEssentials;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Persistence;
using Refit;
using SiblingResources;

public class InfrastructureServiceModule : DependencyServiceModule
{
    public InfrastructureServiceModule()
    {
        PrependModule<PersistenceServiceModule>();
        PrependModule<SiblingResourcesServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var telemetrySettings = new TelemetrySettings();
        configuration.GetSection(nameof(TelemetrySettings)).Bind(telemetrySettings);
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddMemoryCache();
        
        services.AddHealthChecks()
            .AddDbContextCheck<ProjectSchemaDatabaseContext>(name: "sql_server", tags: ["db"])
            .AddCheck<TeamsAggregateApiHealthCheck>("aggr_teams_api", HealthStatus.Unhealthy, 
                tags: ["api", "aggregate"]);
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(telemetrySettings.ServiceName))
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(option => option.SetDbStatementForText = true);
                tracing.AddOtlpExporter(option => option.Endpoint = new Uri(telemetrySettings.TraceExporterEndpoint));
            });
    }
}