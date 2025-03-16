namespace DistribuTe.Framework.ApiEssentials.Odata.Implementations;

using System.Collections.ObjectModel;
using AppEssentials;
using Microsoft.OData.UriParser;

public class OdataFilterVisitor<T>(Func<T> generator) : QueryNodeVisitor<T>
    where T : IWhereClause
{
    private readonly Func<T> _generator = generator ?? throw new ArgumentNullException(nameof(generator));
    
    private T _current = generator();
    private readonly List<T> _filterOptions = [];

    public ReadOnlyCollection<T> FilterOptions => _filterOptions.AsReadOnly();

    public override T Visit(BinaryOperatorNode nodeIn)
    {
        if (nodeIn.OperatorKind != BinaryOperatorKind.And)
        {
            _current.SetOperator(nodeIn.OperatorKind.ToString());
        }
        nodeIn.Right.Accept(this);
        nodeIn.Left.Accept(this);
        
        return _current;
    }
    
    public override T Visit(ConvertNode nodeIn)
    {
        return nodeIn.Source switch
        {
            BinaryOperatorNode binaryNodeIn => Visit(binaryNodeIn),
            SingleValuePropertyAccessNode singleValueNodeIn => Visit(singleValueNodeIn),
            _ => _current
        };
    }

    public override T Visit(SingleValuePropertyAccessNode nodeIn)
    {
        _current.SetFieldName(nodeIn.Property.Name);
        _filterOptions.Add(_current);
        _current = _generator();
        return _current;
    }
    
    public override T Visit(SingleValueFunctionCallNode nodeIn)
    {
        _current.SetOperator(nodeIn.Name);
        var parameters = nodeIn.Parameters.ToArray();
        parameters[1].Accept(this);
        parameters[0].Accept(this);
        return _current;
    }
    
    public override T Visit(ConstantNode nodeIn)
    {
        _current.SetValue(nodeIn.LiteralText);
        return _current;
    }
    
    public override T Visit(CollectionConstantNode nodeIn)
    {
        _current.SetValue(nodeIn.LiteralText);
        return _current;
    }
    
    public override T Visit(InNode nodeIn)
    {
        _current.SetFieldName((nodeIn.Left as SingleValuePropertyAccessNode)!.Property.Name);
        _current.SetOperator(nodeIn.Kind.ToString());
        
        nodeIn.Right.Accept(this);
        _filterOptions.Add(_current);
        
        _current = _generator();
        return _current;
    }
}