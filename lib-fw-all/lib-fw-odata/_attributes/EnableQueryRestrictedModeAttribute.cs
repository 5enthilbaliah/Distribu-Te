// ReSharper disable once CheckNamespace
namespace DistribuTe.Framework.OData.Attributes;

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OData.Common;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

public class EnableQueryRestrictedModeAttribute : EnableQueryAttribute
{
    public EnableQueryRestrictedModeAttribute()
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

        // TODO:: check for override
        MaxTop = 100;
    }
    
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }

    protected virtual ODataQueryOptions GenerateNewQueryOptions(ODataQueryOptions queryOptions)
    {
        throw new NotImplementedException("This method is not implemented. Should never be called.");
    }
    
    protected override ODataQueryOptions CreateQueryOptionsOnExecuting(ActionExecutingContext actionExecutingContext)
    {
        var options = base.CreateQueryOptionsOnExecuting(actionExecutingContext);
        
        return options;
    }



    public override IQueryable ApplyQuery(IQueryable queryable, ODataQueryOptions queryOptions)
    {
        var newOptions = queryOptions;
        var isAggregateRoute = queryOptions.Request.Path.Value!.Contains("aggregates");

        if (isAggregateRoute)
        {
            newOptions = GenerateNewQueryOptions(queryOptions);
        }
        
        return base.ApplyQuery(queryable, newOptions);
    }
    
    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
    {
        var request = actionExecutedContext.HttpContext.Request;
        var querystring = request.QueryString;
        querystring = new QueryString(Regex.Replace(querystring.Value, "[&%24]*skip=\\s*(\\d+)", ""));
        querystring = new QueryString(Regex.Replace(querystring.Value, "[&%24]*top=\\s*(\\d+)", ""));
        actionExecutedContext.HttpContext.Request.QueryString = querystring;
        base.OnActionExecuted(actionExecutedContext);
    }
}