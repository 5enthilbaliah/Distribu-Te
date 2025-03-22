namespace DistribuTe.Framework.AppEssentials;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}