// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.Associates;
using Application.Associates.DataContracts;
using Framework.ApiEssentials.Odata.Implementations;
using Framework.AppEssentials;
using Framework.AppEssentials.Implementations;
using MediatR;
using Microsoft.AspNetCore.Http;

public class AssociateOdataPaginator(ISender sender, IHttpContextAccessor httpContextAccessor,
    OdataFilterVisitor<WhereClauseItem> visitor) : OdataPaginator<AssociateModel>(visitor)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? 
                                                                 throw new ArgumentNullException(nameof(httpContextAccessor));

    public override string Name => "aggregates_associates";
    
    public override async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _sender.Send(new CountAssociatesQuery
        {
            LinqQueryFacade = (GenerateWhereClauseFacadeFrom(_httpContextAccessor.HttpContext!.Request) as LinqQueryFacade)!,
        }, cancellationToken);
    }
}