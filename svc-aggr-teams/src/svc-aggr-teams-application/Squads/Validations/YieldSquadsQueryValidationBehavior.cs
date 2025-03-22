namespace DistribuTe.Aggregates.Teams.Application.Squads.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldSquadsQueryValidationBehavior(EntityLinqMapper<SquadAggregate, SquadId> baseMapper,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper) :
    IPipelineBehavior<YieldSquadsQuery, ErrorOr<IList<SquadModel>>>
{
    private readonly EntityLinqMapper<SquadAggregate, SquadId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));
    
    public async Task<ErrorOr<IList<SquadModel>>> Handle(YieldSquadsQuery request, 
        RequestHandlerDelegate<ErrorOr<IList<SquadModel>>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        var errors = new List<Error>();
        errors.AddRange(_baseMapper.ValidateFilters(facade.WhereClauses, "query_squad"));
        errors.AddRange(_baseMapper.ValidateSortOrders(facade.OrderByClause, "query_squad"));

        if (facade.InnerWhereClauses.TryGetValue("squad_associates", out var innerClauseItems))
        {
            errors.AddRange(_squadSubMapper.ValidateFilters(innerClauseItems, "query_squad_associate"));
        }

        if (errors.Count != 0)
            return errors;
        
        var response = await next();
        return response;
    }
}