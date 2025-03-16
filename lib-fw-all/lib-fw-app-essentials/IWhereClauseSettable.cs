namespace DistribuTe.Framework.AppEssentials;

using System.Collections.ObjectModel;

public enum Operators
{
    EqualTo,
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo,
    NotEqualTo,
    StartsWith,
    EndsWith,
    Contains
}

public interface IWhereClause
{
    string? FieldName { get; } 
    Operators? Operator { get; }
    string? Value { get; }
    
    void SetFieldName(string fieldName);
    void SetOperator(string op);
    void SetValue(string value);
}

public interface IWhereClauseFacade
{
    ReadOnlyCollection<IWhereClause> WhereClauses { get; }
    Dictionary<string, ReadOnlyCollection<IWhereClause>> InnerWhereClauses { get; }
    
    void AddInnerWhereClauses(string projection, ReadOnlyCollection<IWhereClause> clauses);
}