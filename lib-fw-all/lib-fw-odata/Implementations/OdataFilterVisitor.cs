namespace DistribuTe.Framework.OData.Implementations;

using Microsoft.OData.UriParser;

public class OdataFilterVisitor<T> : QueryNodeVisitor<T>
    {
        public List<OdataFilterOption> FilterOptions { get; } = new List<OdataFilterOption>();
        public OdataFilterOption Current { get; private set; } = new OdataFilterOption();

        public override T Visit(BinaryOperatorNode nodeIn)
        {
            if (nodeIn.OperatorKind != BinaryOperatorKind.And)
            {
                Current.Operator = nodeIn.OperatorKind.ToString();
            }
            nodeIn.Right.Accept(this);
            nodeIn.Left.Accept(this);
            return default;
        }

        public override T Visit(ConvertNode nodeIn)
        {
            var binaryNodeIn = nodeIn.Source as BinaryOperatorNode;
            binaryNodeIn.Right.Accept(this);
            binaryNodeIn.Left.Accept(this);
            return default;
        }

        public override T Visit(SingleValuePropertyAccessNode nodeIn)
        {
            Current.FieldName = nodeIn.Property.Name;
            FilterOptions.Add(Current);
            Current = new OdataFilterOption();
            return default;
        }

        public override T Visit(SingleValueFunctionCallNode nodeIn)
        {
            Current.Operator = nodeIn.Name;
            var parameters = nodeIn.Parameters.ToArray();
            parameters[1].Accept(this);
            parameters[0].Accept(this);
            return default;
        }

        public override T Visit(ConstantNode nodeIn)
        {
            Current.Value = nodeIn.LiteralText;
            return default;
        }

        public override T Visit(CollectionConstantNode nodeIn)
        {
            Current.Value = nodeIn.LiteralText;
            return default;
        }

        public override T Visit(InNode nodeIn)
        {
            Current.FieldName = (nodeIn.Left as SingleValuePropertyAccessNode).Property.Name;
            Current.Operator = nodeIn.Kind.ToString();
            nodeIn.Right.Accept(this);
            FilterOptions.Add(Current);
            Current = new OdataFilterOption();
            return default;
        }
    }