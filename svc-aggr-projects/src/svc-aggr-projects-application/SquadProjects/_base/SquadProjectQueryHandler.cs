namespace DistribuTe.Aggregates.Projects.Application.SquadProjects._base;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using Microsoft.EntityFrameworkCore;

public class SquadProjectQueryHandler()
{
    protected Func<IQueryable<SquadProjectAggregate>, IQueryable<SquadProjectAggregate>> FindExpander(EntityLinqFacade facade)
    {
        return (queryable) =>
        {
            if (facade.InnerWhereClauses.TryGetValue("project", out _))
                return queryable.Include(a => a.Project);
            
            return queryable;
        };
    }
}