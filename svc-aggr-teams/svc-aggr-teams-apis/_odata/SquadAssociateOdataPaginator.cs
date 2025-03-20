// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.SquadAssociates;
using Framework.ApiEssentials.Odata;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MediatR;

public class SquadAssociateOdataPaginator(ISender sender, IRequestContext requestContext) : IOdataPaginator
{
    private readonly ISender _sender = sender ?? throw new ArgumentNullException(nameof(sender));
    private readonly IRequestContext _requestContext = requestContext ?? 
                                                       throw new ArgumentNullException(nameof(requestContext));
    
    public string Name => "aggregates_squad_associates";
    
    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        return await _sender.Send(new CountSquadAssociatesQuery
        {
            EntityLinqFacade = _requestContext.GetFeature<EntityLinqFacade>()!,
        }, cancellationToken);
    }
}