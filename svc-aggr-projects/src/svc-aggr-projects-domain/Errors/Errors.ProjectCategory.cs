namespace DistribuTe.Aggregates.Projects.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class ProjectCategories
    {
        public static Error NotFound = Error.NotFound("project_category.not_found",
            "Project category not found.");
    }
}