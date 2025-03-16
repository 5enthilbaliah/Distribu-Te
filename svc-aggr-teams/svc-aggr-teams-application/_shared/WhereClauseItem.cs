namespace DistribuTe.Aggregates.Teams.Application.Shared;

using System.Collections.ObjectModel;
using Framework.AppEssentials;

public class WhereClauseGenerator<T>
    where T : IWhereClauseItem, new()
{
    public static T SpawnOne() => new T();
}

public class WhereClauseItem : IWhereClauseItem
{
    public string? FieldName { get; private set; } 
    public Operators? Operator { get; private set; }
    public string? Value { get; private set; }
    public void SetFieldName(string fieldName)
    {
        FieldName = fieldName;
    }

    public void SetOperator(string op)
    {
        var opLower = op.ToLower();
        Operator = opLower switch
        {
            "equal" => Operators.EqualTo,
            "notequal" => Operators.NotEqualTo,
            "greaterthan" => Operators.GreaterThan,
            "greaterthanorequal" => Operators.GreaterThanOrEqualTo,
            "lessthan" => Operators.LessThan,
            "lessthanorequal" => Operators.LessThanOrEqualTo,
            "startswith" => Operators.StartsWith,
            "endswith" => Operators.EndsWith,
            "contains" => Operators.Contains,
            _ => null
        };
    }

    public void SetValue(string value)
    {
        Value = value.Trim(['\'']);
    }
}