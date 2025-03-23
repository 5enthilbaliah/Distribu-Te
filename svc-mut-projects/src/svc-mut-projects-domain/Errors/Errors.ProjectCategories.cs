namespace DistribuTe.Mutators.Projects.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class ProjectCategories
    {
        public static Error DuplicateCode = Error.Conflict("project_category.duplicate_code", 
            "Project code already exists.");
        public static Error DuplicateName = Error.Conflict("project_category.duplicate_name", 
            "Project name already exists.");
        public static Error NotFound = Error.NotFound("project_category.not_found",
            "Project category not found.");
    }
}