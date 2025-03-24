namespace DistribuTe.Framework.AppEssentials.Linq;

using DataTypes;

public class WhereClauseItem
{
    public string? FieldName { get; private set; } 
    public Operators? Operator { get; private set; }
    public string? Value { get; private set; }
    
    public void SetFieldName(string fieldName)
    {
        FieldName = fieldName.ToLower();
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
    
    public static WhereClauseItem SpawnOne() => new();
}

public class OrderByClauseItem
{
    public string FieldName { get; private set; } = null!;
    public SortDirections Direction { get; private set; }

    public static OrderByClauseItem SpawnOne(string fieldName, SortDirections direction = SortDirections.Ascending )
    {
        return new OrderByClauseItem { FieldName = fieldName, Direction = direction };
    }
}