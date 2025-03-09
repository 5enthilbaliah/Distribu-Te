﻿namespace DistribuTe.Mutators.Teams.Domain.Errors;

using ErrorOr;

public static partial class Errors
{
    public static class SquadAssociates
    {
        public static Error NotFound = Error.NotFound("squad_associate.not_found",
            "Squad associate combination not found.");
    }
}