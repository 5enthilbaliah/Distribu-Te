namespace DistribuTe.Framework.AppEssentials;

using System.Collections.ObjectModel;

public interface IWhereClauseFacade<TWhereClause>
    where TWhereClause : IWhereClause
{
    ReadOnlyCollection<TWhereClause> WhereClauses { get; }
    Dictionary<string, ReadOnlyCollection<TWhereClause>> InnerWhereClauses { get; }
    
    void AddInnerWhereClauses(string projection, ReadOnlyCollection<TWhereClause> clauses);
}