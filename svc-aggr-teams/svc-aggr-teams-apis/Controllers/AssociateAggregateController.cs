namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using Application.Associates;
using Application.Shared;
using Asp.Versioning;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.OData.Attributes;
using Framework.OData.Controllers;
using Framework.OData.Implementations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNetCore.OData.Query;

public class EnableQueryRestrictedModeAttribute<T> : EnableQueryRestrictedModeAttribute 
{
    protected override ODataQueryOptions GenerateNewQueryOptions(ODataQueryOptions queryOptions)
    {
        var defaultAccessor = new HttpContextAccessor();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var mediator = new Mediator(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        var httpRequest = queryOptions.Request;
        
        var path = httpRequest.ODataFeature().Path;
        var model = httpRequest.GetModel();
        var queryContext = new ODataQueryContext(model, typeof(T), path);
        return new ODataQueryOptions<T>(queryContext, httpRequest);
    }
}

[Route("protected/aggregates/associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class AssociateAggregateController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    [Route("")]
    //[EnableQueryRestrictedMode<AssociateModel>()]
    public async Task<IEnumerable<AssociateModel>> SearchAsync(ODataQueryOptions<AssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {

        var visitor = new OdataFilterVisitor<WhereClauseItem>(WhereClauseGenerator<WhereClauseItem>.SpawnOne);
        queryOptions.Filter.FilterClause.Expression.Accept(visitor);
        await Task.CompletedTask;
        return new List<AssociateModel>();
    }
}