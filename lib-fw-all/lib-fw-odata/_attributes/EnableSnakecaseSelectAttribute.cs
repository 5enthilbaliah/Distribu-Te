// ReSharper disable once CheckNamespace
namespace DistribuTe.Framework.OData.Attributes;

using System.Text.RegularExpressions;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            var queryStringCollection = HttpUtility.ParseQueryString(querystring);
            var selectPredicate = queryStringCollection.Get("$select");

            if (!string.IsNullOrEmpty(selectPredicate))
            {
                querystring = querystring.Replace(selectPredicate, selectPredicate.Replace("_", ""));
                request.QueryString = new QueryString(querystring);
            }
        }

        base.OnActionExecuted(actionExecutedContext);
    }
}