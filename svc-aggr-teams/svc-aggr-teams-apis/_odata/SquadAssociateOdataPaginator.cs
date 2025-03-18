// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.SquadAssociates;
using Application.SquadAssociates.DataContracts;
using Framework.ApiEssentials.Odata.Implementations;
using MediatR;
using Microsoft.AspNetCore.Http;

public class SquadAssociateOdataPaginator(ISender sender, IHttpContextAccessor httpContextAccessor,
    OdataFilterVisitor visitor) : OdataPaginator<SquadAssociateModel>(visitor)
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? 
                                                                 throw new ArgumentNullException(nameof(httpContextAccessor));
    
    public override string Name => "aggregates_squad_associates";
    
    public override async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _sender.Send(new CountSquadAssociatesQuery
        {
            LinqQueryFacade = GenerateWhereClauseFacadeFrom(_httpContextAccessor.HttpContext!.Request),
        }, cancellationToken);
    }
}