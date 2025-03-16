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

public interface IWhereClauseSettable
{
    void SetFieldName(string fieldName);
    void SetOperator(string op);
    void SetValue(string value);
}

public interface IWhereClauseGettable
{
    string? FieldName { get; } 
    Operators? Operator { get; }
    string? Value { get; }
}

public interface IWhereClauseFacade
{
    ReadOnlyCollection<IWhereClauseGettable> WhereClauses { get; }
}