namespace DistribuTe.Mutators.Teams.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class Associates
    {
        public static Error DuplicateEmail = Error.Conflict("associate.duplicate_email", 
            "Email address already exists.");
        public static Error NotFound = Error.NotFound("associate.not_found",
            "Associate not found.");
    }
}