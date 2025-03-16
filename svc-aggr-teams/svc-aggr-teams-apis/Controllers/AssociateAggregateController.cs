namespace DistribuTe.Aggregates.Teams.Apis.Controllers;

using Application.Associates;
using Application.Shared;
using Asp.Versioning;
using Framework.ApiEssentials.Odata.Controllers;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

[Route("protected/aggregates/associates")]
[ApiVersion("1.0")]
[Produces("application/json")]
public class AssociateAggregateController(IMediator mediator) : DistribuTeController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet]
    [Route("")]
    public async Task<IEnumerable<AssociateModel>> SearchAsync(ODataQueryOptions<AssociateModel> queryOptions,
        CancellationToken cancellationToken = default)
    {

        var visitor = new OdataFilterVisitor<WhereClauseItem>(WhereClauseGenerator<WhereClauseItem>.SpawnOne);
        queryOptions.Filter.FilterClause.Expression.Accept(visitor);
        var facade = new WhereClauseFacade(visitor.FilterOptions
            .Select(IWhereClause (x) => x).ToList());
        await Task.CompletedTask;
        return new List<AssociateModel>();
    }
}