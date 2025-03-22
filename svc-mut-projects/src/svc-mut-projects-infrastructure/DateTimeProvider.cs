namespace DistribuTe.Mutators.Projects.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Framework.AppEssentials;

[ExcludeFromCodeCoverage]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}