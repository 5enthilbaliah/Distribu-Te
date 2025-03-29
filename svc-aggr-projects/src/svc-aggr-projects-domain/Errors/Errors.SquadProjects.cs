namespace DistribuTe.Aggregates.Projects.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class SquadProjects
    {
        public static Error NotFound = Error.NotFound("squad_project.not_found",
            "Squad project combination not found.");
    }
}