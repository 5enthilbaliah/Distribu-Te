namespace DistribuTe.Aggregates.Teams.Application.Associates;

using DataContracts;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldAssociatesQuery : IRequest<ErrorOr<IList<AssociateModel>>>
{
    public EntityLinqFacade EntityLinqFacade { get; init; } = null!;
    public int Skip { get; set; } = 0;
    public int Take { get; set; } = 100;
}