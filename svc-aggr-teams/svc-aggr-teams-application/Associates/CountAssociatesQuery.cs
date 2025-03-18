namespace DistribuTe.Aggregates.Teams.Application.Associates;

using Framework.AppEssentials.Linq;
using MediatR;

public class CountAssociatesQuery : IRequest<long>
{
    public LinqQueryFacade LinqQueryFacade { get; set; } = null!;
}