namespace DistribuTe.Framework.ApiEssentials.Odata.Filters;

using Microsoft.AspNetCore.OData.Query;

public class DistribuTeEnableQueryAttribute : EnableQueryAttribute
{
    public DistribuTeEnableQueryAttribute()
    {
        AllowedLogicalOperators = AllowedLogicalOperators.And |
                                  AllowedLogicalOperators.Equal |
                                  AllowedLogicalOperators.GreaterThan |
                                  AllowedLogicalOperators.GreaterThanOrEqual |
                                  AllowedLogicalOperators.LessThan |
                                  AllowedLogicalOperators.LessThanOrEqual |
                                  AllowedLogicalOperators.NotEqual;

        AllowedArithmeticOperators = AllowedArithmeticOperators.None;

        MaxExpansionDepth = 1;

        AllowedFunctions = AllowedFunctions.EndsWith | AllowedFunctions.StartsWith | AllowedFunctions.Contains;

        MaxTop = 100;
    }
}