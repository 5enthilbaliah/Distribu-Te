namespace DistribuTe.Aggregates.Projects.Application.SquadProjects.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldSquadProjectsQueryValidationBehavior(EntityLinqMapper<SquadProjectAggregate, SquadProjectId> baseMapper) :
    IPipelineBehavior<YieldSquadProjectsQuery, ErrorOr<IList<SquadProjectModel>>>
{
    private readonly EntityLinqMapper<SquadProjectAggregate, SquadProjectId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<ErrorOr<IList<SquadProjectModel>>> Handle(YieldSquadProjectsQuery request, 
        RequestHandlerDelegate<ErrorOr<IList<SquadProjectModel>>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        var errors = new List<Error>();
        errors.AddRange(_baseMapper.ValidateFilters(facade.WhereClauses, "query_squad_project"));
        errors.AddRange(_baseMapper.ValidateSortOrders(facade.OrderByClause, "query_squad_project"));
        
        if (errors.Count != 0)
            return errors;
        
        var response = await next();
        return response;
    }
}