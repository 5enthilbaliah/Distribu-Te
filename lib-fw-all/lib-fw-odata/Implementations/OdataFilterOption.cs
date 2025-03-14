namespace DistribuTe.Framework.OData.Implementations;

using Microsoft.OData.UriParser;

public class OdataFilterOption
{
    public string Operator { get; set; }
    public string Value { get; set; }
    public string FieldName { get; set; }
}