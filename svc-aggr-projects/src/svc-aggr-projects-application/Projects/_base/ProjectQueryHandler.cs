namespace DistribuTe.Aggregates.Projects.Application.Projects._base;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using Microsoft.EntityFrameworkCore;

public class ProjectQueryHandler(EntityLinqMapper<SquadProjectAggregate, SquadProjectId> squadSubMapper)
{
    private readonly EntityLinqMapper<SquadProjectAggregate, SquadProjectId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));

    protected Func<IQueryable<ProjectAggregate>, IQueryable<ProjectAggregate>> FindExpander(EntityLinqFacade facade)
    {
        return (queryable) =>
        {
            if (facade.InnerWhereClauses.TryGetValue("project_categories", out _))
            {
                queryable = queryable.Include(a => a.Category);
            }
            
            if (!facade.InnerWhereClauses.TryGetValue("squad_projects", out var clause))
                return queryable;
                
            var squadProjectExpr = _squadSubMapper.MapAsSearchExpression(clause);
            if (squadProjectExpr != null)
            {
                return queryable.Include(a => 
                    a.SquadProjects.AsQueryable().Where(squadProjectExpr));
            }
                
            return queryable.Include(a => a.SquadProjects);
        };
    }
}