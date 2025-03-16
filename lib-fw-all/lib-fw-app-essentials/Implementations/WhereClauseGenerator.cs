namespace DistribuTe.Framework.AppEssentials.Implementations;

public abstract class WhereClauseGenerator<TWhereClause>
    where TWhereClause : IWhereClause, new()
{
    public static TWhereClause SpawnOne() => new();
}