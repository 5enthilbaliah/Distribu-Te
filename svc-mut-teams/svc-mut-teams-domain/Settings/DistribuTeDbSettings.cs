namespace DistribuTe.Mutators.Teams.Domain.Settings;

public class DistribuTeDbSettings
{
    public string ConnectionString { get; set; } = null!;
    public int TimeoutInSeconds { get; set; }
}