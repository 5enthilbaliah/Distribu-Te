namespace DistribuTe.Framework.ApiEssentials.Telemetry;

public class HealthCheckReportEntry
{
    public IReadOnlyDictionary<string, object> Data { get; set; } = null!;
    public string? Description { get; set; }
    public TimeSpan Duration { get; set; }
    public string? Exception { get; set; }
    public HealthCheckStatus Status { get; set; }
    public IEnumerable<string>? Tags { get; set; }
}