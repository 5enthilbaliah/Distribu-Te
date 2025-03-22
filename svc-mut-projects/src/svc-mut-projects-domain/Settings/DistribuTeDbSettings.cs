namespace DistribuTe.Mutators.Projects.Domain.Settings;

public class DistribuTeDbSettings
{
    public string ConnectionString { get; init; } = null!;
    public int TimeoutInSeconds { get; init; }
}