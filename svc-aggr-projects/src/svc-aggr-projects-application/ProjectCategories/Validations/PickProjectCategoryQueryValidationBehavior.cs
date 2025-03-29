namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickProjectCategoryQueryValidationBehavior(
    EntityLinqMapper<ProjectAggregate, ProjectId> projectSubMapper) :
    IPipelineBehavior<PickProjectCategoryQuery, ErrorOr<ProjectCategoryModel>>
{
    private readonly EntityLinqMapper<ProjectAggregate, ProjectId> _projectSubMapper =
        projectSubMapper ?? throw new ArgumentNullException(nameof(projectSubMapper));


    public async Task<ErrorOr<ProjectCategoryModel>> Handle(PickProjectCategoryQuery request, 
        RequestHandlerDelegate<ErrorOr<ProjectCategoryModel>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        if (facade.WhereClauses.Any())
            return Error.Conflict("query_project_category.invalid_combination", 
                "Filter(s) cannot be used select by id request.");
        
        var errors = new List<Error>();
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