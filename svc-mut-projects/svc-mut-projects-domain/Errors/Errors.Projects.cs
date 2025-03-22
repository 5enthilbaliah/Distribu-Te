namespace DistribuTe.Mutators.Projects.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class Projects
    {
        public static Error DuplicateCode = Error.Conflict("project.duplicate_code", 
            "Project code already exists.");
        public static Error NotFound = Error.NotFound("project.not_found",
            "Project not found.");
    }
}