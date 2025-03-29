namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldProjectCategoriesQueryValidationBehavior(EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId> baseMapper,
    EntityLinqMapper<ProjectAggregate, ProjectId> projectSubMapper) :
    IPipelineBehavior<YieldProjectCategoriesQuery, ErrorOr<IList<ProjectCategoryModel>>>
{
    private readonly EntityLinqMapper<ProjectCategoryAggregate, ProjectCategoryId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    private readonly EntityLinqMapper<ProjectAggregate, ProjectId> _projectSubMapper =
        projectSubMapper ?? throw new ArgumentNullException(nameof(projectSubMapper));
    
    public async Task<ErrorOr<IList<ProjectCategoryModel>>> Handle(YieldProjectCategoriesQuery request, 
        RequestHandlerDelegate<ErrorOr<IList<ProjectCategoryModel>>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        var errors = new List<Error>();
        errors.AddRange(_baseMapper.ValidateFilters(facade.WhereClauses, "query_project_category"));
        errors.AddRange(_baseMapper.ValidateSortOrders(facade.OrderByClause, "query_project_category"));

        if (facade.InnerWhereClauses.TryGetValue("projects", out var innerClauseItems))
        {
            errors.AddRange(_projectSubMapper.ValidateFilters(innerClauseItems, "query_project"));
        }

        if (errors.Count != 0)
            return errors;
        
        var response = await next();
        return response;
    }
}