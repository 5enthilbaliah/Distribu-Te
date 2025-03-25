namespace DistribuTe.Framework.ApiEssentials.Telemetry;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using ModuleZ;

public class TelemetryPipeline : IMiddlewarePipeline
{
    public void Setup(WebApplication app, IWebHostEnvironment environment, IConfiguration configuration)
    {
        app.MapHealthChecks("health", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckResponseWriter.WriteHealthCheckResponse
        });

        app.UseOpenTelemetryPrometheusScrapingEndpoint();
    }
}