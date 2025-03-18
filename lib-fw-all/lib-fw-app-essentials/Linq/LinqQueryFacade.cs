namespace DistribuTe.Framework.AppEssentials.Linq;

using System.Collections.ObjectModel;

public class LinqQueryFacade(ReadOnlyCollection<WhereClauseItem> whereClauses, int? skip, int? top)
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