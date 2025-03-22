namespace DistribuTe.Aggregates.Teams.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class Squads
    {
        public static Error NotFound = Error.NotFound("squad.not_found",
            "Squad not found.");
    }
}