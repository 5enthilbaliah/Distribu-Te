namespace DistribuTe.Mutators.Projects.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using Application.Shared;

[ExcludeFromCodeCoverage]
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}