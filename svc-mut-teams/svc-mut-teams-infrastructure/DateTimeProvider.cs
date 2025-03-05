namespace DistribuTe.Mutators.Teams.Infrastructure;

using Application.Shared;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}