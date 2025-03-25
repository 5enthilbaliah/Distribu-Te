namespace DistribuTe.Framework.ApiEssentials.Telemetry;

using Microsoft.Extensions.Diagnostics.HealthChecks;

public class HealthCheckReport(Dictionary<string, HealthCheckReportEntry> entries)
{
    public HealthCheckStatus Status { get; set; }
    // ReSharper disable once MemberCanBePrivate.Global
    public Dictionary<string, HealthCheckReportEntry> Entries { get; } = entries;

    public static HealthCheckReport CreateFrom(HealthReport report, Func<Exception, string>? exceptionMessage = null)
    {
        var uiReport = new HealthCheckReport(new Dictionary<string, HealthCheckReportEntry>())
        {
            Status = (HealthCheckStatus)report.Status,
        };

        foreach (var item in report.Entries)
        {
            var entry = new HealthCheckReportEntry
            {
                Data = item.Value.Data,
                Description = item.Value.Description,
                Duration = item.Value.Duration,
                Status = (HealthCheckStatus)item.Value.Status
            };

            if (item.Value.Exception != null)
            {
                var message = exceptionMessage == null ? item.Value.Exception?.Message : exceptionMessage(item.Value.Exception);

                entry.Exception = message;
                entry.Description = item.Value.Description ?? message;
            }

            entry.Tags = item.Value.Tags;

            uiReport.Entries.Add(item.Key, entry);
        }

        return uiReport;
    }

    public static HealthCheckReport CreateFrom(Exception exception, string entryName = "Endpoint")
    {
        var uiReport = new HealthCheckReport(new Dictionary<string, HealthCheckReportEntry>())
        {
            Status = HealthCheckStatus.Unhealthy,
        };

        uiReport.Entries.Add(entryName, new HealthCheckReportEntry
        {
            Exception = exception.Message,
            Description = exception.Message,
            Duration = TimeSpan.FromSeconds(0),
            Status = HealthCheckStatus.Unhealthy
        });

        return uiReport;
    }
}