namespace DistribuTe.Mutators.Teams.Domain;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public static string AuthorizationKey => "Authorization";
    public static string CorrelationKey => "x-correlation-id";
    public static string VersionKey => "x-version";
}