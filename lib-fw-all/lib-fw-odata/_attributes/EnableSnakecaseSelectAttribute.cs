// ReSharper disable once CheckNamespace
namespace DistribuTe.Framework.OData.Attributes;

using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.OData.Query;

public class HandleSnakeSelectAttribute : EnableQueryAttribute
{
    public HandleSnakeSelectAttribute() : base()
    { }

    public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
    {
        var request = actionExecutedContext.HttpContext.Request;
        var querystring = request.QueryString.Value;

        if (!string.IsNullOrEmpty(querystring))
        {
            var matches = Regex.Matches(querystring, "[&%24]*select=\\s*[(\\w+),]+");
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    var replaceWith = match.Value.Replace("_", "");
                    querystring = querystring.Replace(match.Value, replaceWith);
                }
                request.QueryString = new QueryString(querystring);
            }
        }

        base.OnActionExecuted(actionExecutedContext);
    }
}