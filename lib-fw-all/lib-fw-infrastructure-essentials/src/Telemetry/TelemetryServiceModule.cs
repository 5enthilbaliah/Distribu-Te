namespace DistribuTe.Framework.InfrastructureEssentials.Telemetry;

using DomainEssentials.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleZ.Implementations;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public class TelemetryServiceModule : DependencyServiceModule
{
    protected override void RegisterCurrent(IServiceCollection services, IWebHostEnvironment environment, 
        IConfiguration configuration)
    {
        var telemetrySettings = new TelemetrySettings();
        configuration.GetSection(nameof(TelemetrySettings)).Bind(telemetrySettings);
        
        services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(telemetrySettings.ServiceName))
            .WithTracing(tracing =>
            {
                tracing.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation(option => option.SetDbStatementForText = true)
                    .AddOtlpExporter(option =>
                    {
                        option.Endpoint = new Uri(telemetrySettings.TraceExporterEndpoint);
                    });
            }).WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation()
                    .AddRuntimeInstrumentation()
                    .AddProcessInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddPrometheusExporter();
            }).WithLogging(logging =>
            {
                logging.AddOtlpExporter(option =>
                {
                    option.Endpoint = new Uri(telemetrySettings.LogExporterEndpoint);
                    option.Headers = $"X-Seq-ApiKey={telemetrySettings.LogApiKey}";
                    option.Protocol = OtlpExportProtocol.HttpProtobuf;
                });
            });
    }
}