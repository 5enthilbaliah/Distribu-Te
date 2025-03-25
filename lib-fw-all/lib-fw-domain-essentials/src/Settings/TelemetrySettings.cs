namespace DistribuTe.Framework.DomainEssentials.Settings;

public class TelemetrySettings
{
    public string ServiceName { get; init; } = null!;
    public string TraceExporterEndpoint { get; init; } = null!;
}