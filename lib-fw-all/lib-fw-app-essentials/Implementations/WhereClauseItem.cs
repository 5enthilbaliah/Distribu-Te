namespace DistribuTe.Framework.AppEssentials.Implementations;

using System.Collections.ObjectModel;

public class WhereClauseItem : IWhereClause
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
}

public class WhereClauseFacade(ReadOnlyCollection<WhereClauseItem> whereClauses) : IWhereClauseFacade<WhereClauseItem>
{
    public ReadOnlyCollection<WhereClauseItem> WhereClauses => whereClauses;
    public Dictionary<string, ReadOnlyCollection<WhereClauseItem>> InnerWhereClauses { get; } = new();

    public void AddInnerWhereClauses(string projection, ReadOnlyCollection<WhereClauseItem> clauses)
    {
        InnerWhereClauses.Add(projection, clauses);
    }
}
