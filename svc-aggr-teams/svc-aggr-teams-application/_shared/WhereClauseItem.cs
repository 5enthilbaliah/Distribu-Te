namespace DistribuTe.Aggregates.Teams.Application.Shared;

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

public class WhereClauseItem
{
    public string FieldName { get; set; } = null!;
    public Operators Operator { get; set; }
    public object Value { get; set; } = null!;
}