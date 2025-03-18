namespace DistribuTe.Framework.ApiEssentials.Odata.Implementations;

using System.Collections.ObjectModel;
using AppEssentials.Linq;
using Microsoft.OData.UriParser;

public class OdataFilterVisitor(Func<WhereClauseItem> generator) : QueryNodeVisitor<WhereClauseItem>
{
    private readonly Func<WhereClauseItem> _generator = generator ?? throw new ArgumentNullException(nameof(generator));
    
    private WhereClauseItem _current = generator();
    private readonly List<WhereClauseItem> _filterOptions = [];

    public ReadOnlyCollection<WhereClauseItem> FilterOptions => _filterOptions.AsReadOnly();

    public override WhereClauseItem Visit(BinaryOperatorNode nodeIn)
    {
        if (nodeIn.OperatorKind != BinaryOperatorKind.And)
        {
            _current.SetOperator(nodeIn.OperatorKind.ToString());
        }
        nodeIn.Right.Accept(this);
        nodeIn.Left.Accept(this);
        
        return _current;
    }
    
    public override WhereClauseItem Visit(ConvertNode nodeIn)
    {
        return nodeIn.Source switch
        {
            BinaryOperatorNode binaryNodeIn => Visit(binaryNodeIn),
            SingleValuePropertyAccessNode singleValueNodeIn => Visit(singleValueNodeIn),
            _ => _current
        };
    }

    public override WhereClauseItem Visit(SingleValuePropertyAccessNode nodeIn)
    {
        _current.SetFieldName(nodeIn.Property.Name);
        _filterOptions.Add(_current);
        _current = _generator();
        return _current;
    }
    
    public override WhereClauseItem Visit(SingleValueFunctionCallNode nodeIn)
    {
        _current.SetOperator(nodeIn.Name);
        var parameters = nodeIn.Parameters.ToArray();
        parameters[1].Accept(this);
        parameters[0].Accept(this);
        return _current;
    }
    
    public override WhereClauseItem Visit(ConstantNode nodeIn)
    {
        _current.SetValue(nodeIn.LiteralText);
        return _current;
    }
    
    public override WhereClauseItem Visit(CollectionConstantNode nodeIn)
    {
        _current.SetValue(nodeIn.LiteralText);
        return _current;
    }
    
    public override WhereClauseItem Visit(InNode nodeIn)
    {
        _current.SetFieldName((nodeIn.Left as SingleValuePropertyAccessNode)!.Property.Name);
        _current.SetOperator(nodeIn.Kind.ToString());
        
        nodeIn.Right.Accept(this);
        _filterOptions.Add(_current);
        
        _current = _generator();
        return _current;
    }
}