// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Application.Squads.Base;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using Microsoft.EntityFrameworkCore;

public class SquadQueryHandler(EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper)
{
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));
    
    protected Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>> FindExpander(EntityLinqFacade facade)
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