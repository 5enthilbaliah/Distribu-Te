// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Application.Associates.Base;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using Microsoft.EntityFrameworkCore;

public class AssociateQueryHandler(EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper)
{
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));

    protected Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>> FindExpander(EntityLinqFacade facade)
    {
        return (queryable) =>
        {
            if (!facade.InnerWhereClauses.TryGetValue("squad_associates", out var clause))
                return queryable;
                
            var squadAssociateExpr = _squadSubMapper.MapAsSearchExpression(clause);
            if (squadAssociateExpr != null)
            {
                return queryable.Include(a => 
                    a.SquadAssociates.AsQueryable().Where(squadAssociateExpr));
            }
                
            return queryable.Include(a => a.SquadAssociates);
        };
    }
}