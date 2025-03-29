namespace DistribuTe.Aggregates.Projects.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class Projects
    {
        public static Error NotFound = Error.NotFound("project.not_found",
            "Project not found.");
    }
}