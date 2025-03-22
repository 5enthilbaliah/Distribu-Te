namespace DistribuTe.Framework.InfrastructureEssentials;

using System.Diagnostics.CodeAnalysis;
using AppEssentials;

[ExcludeFromCodeCoverage]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}