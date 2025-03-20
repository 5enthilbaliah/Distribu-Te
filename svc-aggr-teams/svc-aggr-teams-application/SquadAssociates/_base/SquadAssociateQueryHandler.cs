// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates.Base;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using Microsoft.EntityFrameworkCore;

public class SquadAssociateQueryHandler
{
    protected Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>> FindExpander(EntityLinqFacade facade)
    {
        return (queryable) =>
        {
            if (facade.InnerWhereClauses.TryGetValue("squad", out _))
            {
                queryable = queryable.Include(a => a.Squad);
            }
            
            if (facade.InnerWhereClauses.TryGetValue("associate", out _))
            {
                queryable = queryable.Include(a => a.Associate);
            }
            
            return queryable;
        };
    }
}