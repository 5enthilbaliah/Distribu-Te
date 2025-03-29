namespace DistribuTe.Aggregates.Projects.Application.Projects.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickProjectQueryValidationBehavior(
    EntityLinqMapper<SquadProjectAggregate, SquadProjectId> squadSubMapper) :
    IPipelineBehavior<PickProjectQuery, ErrorOr<ProjectModel>>
{
    private readonly EntityLinqMapper<SquadProjectAggregate, SquadProjectId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));


    public async Task<ErrorOr<ProjectModel>> Handle(PickProjectQuery request, 
        RequestHandlerDelegate<ErrorOr<ProjectModel>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        if (facade.WhereClauses.Any())
            return Error.Conflict("query_project.invalid_combination", 
                "Filter(s) cannot be used select by id request.");
        
        var errors = new List<Error>();
        if (facade.InnerWhereClauses.TryGetValue("squad_projects", out var innerClauseItems))
        {
            errors.AddRange(_squadSubMapper.ValidateFilters(innerClauseItems, "query_squad_project"));
        }

        if (errors.Count != 0)
            return errors;
        
        var response = await next();
        return response;
    }
}