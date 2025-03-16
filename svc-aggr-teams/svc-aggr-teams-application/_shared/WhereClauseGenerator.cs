namespace DistribuTe.Aggregates.Teams.Application.Shared;

using Framework.AppEssentials;

public abstract class WhereClauseGenerator<T>
    where T : IWhereClauseSettable, new()
{
    public static T SpawnOne() => new();
}