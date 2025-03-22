// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Projects.Application.Shared;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}