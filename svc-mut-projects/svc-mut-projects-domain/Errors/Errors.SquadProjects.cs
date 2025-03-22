namespace DistribuTe.Mutators.Projects.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class SquadProjects
    {
        public static Error NotFound = Error.NotFound("squad_project.not_found",
            "Squad project combination not found.");
        public static Error DuplicateAllocation = Error.Conflict("squad_project.duplicate_allocation",
            "Squad allocation already exists for the project.");
    }
}