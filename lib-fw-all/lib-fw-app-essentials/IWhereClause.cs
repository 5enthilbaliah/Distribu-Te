namespace DistribuTe.Framework.AppEssentials;

public interface IWhereClause
{
    string? FieldName { get; } 
    Operators? Operator { get; }
    string? Value { get; }
    
    void SetFieldName(string fieldName);
    void SetOperator(string op);
    void SetValue(string value);
}