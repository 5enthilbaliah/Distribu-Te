namespace DistribuTe.Mutators.Projects.Apis.Controllers.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class SquadProjectEndpoints
    {
        public static Error MismatchProjectId = Error.Validation("api_squad_project.mismatch_project_id",
            "Project id in route and body should match.");
        public static Error MismatchSquadId = Error.Validation("api_squad_project.mismatch_squad_id",
            "Squad id in route and body should match.");
    }
}