namespace DistribuTe.Framework.AppEssentials.Implementations;

using System.Collections.ObjectModel;

public class LinqQueryFacade(ReadOnlyCollection<WhereClauseItem> whereClauses, int? skip, int? top) : ILinqQueryFacade<WhereClauseItem>
{
    public ReadOnlyCollection<WhereClauseItem> WhereClauses => whereClauses;
    public Dictionary<string, ReadOnlyCollection<WhereClauseItem>> InnerWhereClauses { get; } = new();

    public int Skip { get; } = skip ?? 0;
    public int Top { get; } = top ?? 50;

    public void AddInnerWhereClauses(string projection, ReadOnlyCollection<WhereClauseItem> clauses)
    {
        InnerWhereClauses.Add(projection, clauses);
    }
}