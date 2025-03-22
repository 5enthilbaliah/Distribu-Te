namespace DistribuTe.Framework.AppEssentials.Linq;

using System.Collections.ObjectModel;

public class EntityLinqFacade(ReadOnlyCollection<WhereClauseItem> whereClauses, 
    ReadOnlyCollection<OrderByClauseItem> orderByClauses,
    int? skip, int? top)
{
    public ReadOnlyCollection<WhereClauseItem> WhereClauses => whereClauses;
    public ReadOnlyCollection<OrderByClauseItem> OrderByClause => orderByClauses;
    
    public Dictionary<string, ReadOnlyCollection<WhereClauseItem>> InnerWhereClauses { get; } = new();

    public int? Skip { get; } = skip;
    public int? Top { get; } = top;

    public void AddInnerWhereClauses(string projection, ReadOnlyCollection<WhereClauseItem> clauses)
    {
        InnerWhereClauses.Add(projection, clauses);
    }
}