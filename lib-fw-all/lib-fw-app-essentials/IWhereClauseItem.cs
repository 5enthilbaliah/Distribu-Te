namespace DistribuTe.Framework.AppEssentials;

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

public interface IWhereClauseItem
{
    string? FieldName { get; }
    Operators? Operator { get; }
    string? Value { get; }

    void SetFieldName(string fieldName);
    void SetOperator(string op);
    void SetValue(string value);
}