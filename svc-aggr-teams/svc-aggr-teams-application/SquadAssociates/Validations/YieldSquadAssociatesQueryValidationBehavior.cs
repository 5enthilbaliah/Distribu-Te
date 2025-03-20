namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class YieldSquadAssociatesQueryValidationBehavior(EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> baseMapper) :
    IPipelineBehavior<YieldSquadAssociatesQuery, ErrorOr<IList<SquadAssociateModel>>>
{
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _baseMapper =
        baseMapper ?? throw new ArgumentNullException(nameof(baseMapper));
    
    public async Task<ErrorOr<IList<SquadAssociateModel>>> Handle(YieldSquadAssociatesQuery request, 
        RequestHandlerDelegate<ErrorOr<IList<SquadAssociateModel>>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        var errors = new List<Error>();
        errors.AddRange(_baseMapper.ValidateFilters(facade.WhereClauses, "squad_associate"));
        errors.AddRange(_baseMapper.ValidateSortOrders(facade.OrderByClause, "squad_associate"));
        
        if (errors.Count != 0)
            return errors;
        
        var response = await next();
        return response;
    }
}