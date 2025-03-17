namespace DistribuTe.Aggregates.Teams.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class Associates
    {
        public static Error NotFound = Error.NotFound("associate.not_found",
            "Associate not found.");
    }
}