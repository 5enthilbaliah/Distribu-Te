namespace DistribuTe.Aggregates.Teams.Application.Associates.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldAssociatesQueryValidationBehavior(EntityLinqMapper<AssociateAggregate, AssociateId> baseMapper,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper) :
    IPipelineBehavior<YieldAssociatesQuery, ErrorOr<IList<AssociateModel>>>
{
    private readonly EntityLinqMapper<AssociateAggregate, AssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));
    
    public async Task<ErrorOr<IList<AssociateModel>>> Handle(YieldAssociatesQuery request, 
        RequestHandlerDelegate<ErrorOr<IList<AssociateModel>>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        var errors = new List<Error>();
        errors.AddRange(_baseMapper.ValidateFilters(facade.WhereClauses, "associate"));
        errors.AddRange(_baseMapper.ValidateSortOrders(facade.OrderByClause, "associate"));

        if (facade.InnerWhereClauses.TryGetValue("squad_associates", out var innerClauseItems))
        {
            errors.AddRange(_squadSubMapper.ValidateFilters(innerClauseItems, "squad_associate"));
        }

        if (errors.Count != 0)
            return errors;
        
        var response = await next();
        return response;
    }
}