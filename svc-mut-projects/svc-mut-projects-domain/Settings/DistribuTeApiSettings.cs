namespace DistribuTe.Mutators.Projects.Domain.Settings;

public class DistribuTeApiSettings
{
    public string TeamsAggregateApiBaseUrl { get; init; } = null!;
    public int TimeoutInSeconds { get; init; }
}