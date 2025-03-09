namespace DistribuTe.Mutators.Teams.Apis.Controllers.Errors;

using ErrorOr;

public static class Errors
{
    public static class SquadAssociateEndpoints
    {
        public static Error MismatchAssociateId = Error.Validation("squad_associate_endpoint.mismatch_associate_id",
            "Associate id in route and body should match.");
        public static Error MismatchSquadId = Error.Validation("squad_associate_endpoint.mismatch_squad_id",
            "Squad id in route and body should match.");
    }
}