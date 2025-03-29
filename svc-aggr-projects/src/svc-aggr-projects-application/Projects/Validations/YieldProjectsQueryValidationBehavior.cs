namespace DistribuTe.Aggregates.Projects.Application.Projects.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldProjectsQueryValidationBehavior(EntityLinqMapper<ProjectAggregate, ProjectId> baseMapper,
    EntityLinqMapper<SquadProjectAggregate, SquadProjectId> squadSubMapper) :
    IPipelineBehavior<YieldProjectsQuery, ErrorOr<IList<ProjectModel>>>
{
    private readonly EntityLinqMapper<ProjectAggregate, ProjectId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    private readonly EntityLinqMapper<SquadProjectAggregate, SquadProjectId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));
    
    public async Task<ErrorOr<IList<ProjectModel>>> Handle(YieldProjectsQuery request, 
        RequestHandlerDelegate<ErrorOr<IList<ProjectModel>>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        var errors = new List<Error>();
        errors.AddRange(_baseMapper.ValidateFilters(facade.WhereClauses, "query_project"));
        errors.AddRange(_baseMapper.ValidateSortOrders(facade.OrderByClause, "query_project"));

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