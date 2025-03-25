using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(assemblyName: "svc-mut-teams-unit-tests")]
namespace DistribuTe.Mutators.Teams.Infrastructure;

using Framework.AppEssentials;
using Framework.DomainEssentials.Settings;
using Framework.ModuleZ.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Persistence;

public class InfrastructureServiceModule : DependencyServiceModule
{
    public InfrastructureServiceModule()
    {
        PrependModule<PersistenceServiceModule>();
    }
    
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var telemetrySettings = new TelemetrySettings();
        configuration.GetSection(nameof(TelemetrySettings)).Bind(telemetrySettings);
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddMemoryCache();
        
        services.AddHealthChecks()
            .AddDbContextCheck<TeamSchemaDatabaseContext>(name: "sql_server", tags: ["db"]);

        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(telemetrySettings.ServiceName))
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(option => option.SetDbStatementForText = true);
                tracing.AddOtlpExporter(option => option.Endpoint = new Uri(telemetrySettings.TraceExporterEndpoint));
            }).WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddPrometheusExporter();
            });
    }
}