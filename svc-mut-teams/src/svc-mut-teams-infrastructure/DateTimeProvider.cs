namespace DistribuTe.Mutators.Teams.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Framework.AppEssentials;

[ExcludeFromCodeCoverage]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}