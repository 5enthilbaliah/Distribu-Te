namespace DistribuTe.Framework.AppEssentials;

using System.Collections.ObjectModel;

public interface ILinqQueryFacade<TWhereClause>
    where TWhereClause : IWhereClause
{
    ReadOnlyCollection<TWhereClause> WhereClauses { get; }
    Dictionary<string, ReadOnlyCollection<TWhereClause>> InnerWhereClauses { get; }
    
    int Skip { get; }
    int Top { get; }
    
    void AddInnerWhereClauses(string projection, ReadOnlyCollection<TWhereClause> clauses);
}