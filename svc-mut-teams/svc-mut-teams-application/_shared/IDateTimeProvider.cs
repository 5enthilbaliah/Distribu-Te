// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Application.Shared;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}