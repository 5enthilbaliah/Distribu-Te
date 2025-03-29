namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories._base;

using Domain.Entities;
using Framework.AppEssentials.Linq;
using Microsoft.EntityFrameworkCore;

public class ProjectCategoryQueryHandler(EntityLinqMapper<ProjectAggregate, ProjectId> projectSubMapper)
{
    private readonly EntityLinqMapper<ProjectAggregate, ProjectId> _projectSubMapper =
        projectSubMapper ?? throw new ArgumentNullException(nameof(projectSubMapper));

    protected Func<IQueryable<ProjectCategoryAggregate>, IQueryable<ProjectCategoryAggregate>> FindExpander(EntityLinqFacade facade)
    {
        return (queryable) =>
        {
            if (!facade.InnerWhereClauses.TryGetValue("projects", out var clause))
                return queryable;
                
            var projectExpr = _projectSubMapper.MapAsSearchExpression(clause);
            if (projectExpr != null)
            {
                return queryable.Include(a => 
                    a.Projects.AsQueryable().Where(projectExpr));
            }
                
            return queryable.Include(a => a.Projects);
        };
    }
}