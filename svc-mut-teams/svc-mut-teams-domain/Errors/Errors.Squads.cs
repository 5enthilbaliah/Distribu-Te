namespace DistribuTe.Mutators.Teams.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class Squads
    {
        public static Error DuplicateCode => Error.Conflict("squad.duplicate_code",
            "Squad code already exists."); 
        public static Error DuplicateName => Error.Conflict("squad.duplicate_name",
            "Squad name already exists."); 
        public static Error NotFound = Error.NotFound("squad.not_found",
            "Squad not found.");
    }
}