namespace DistribuTe.Aggregates.Teams.Application.Associates.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickAssociateQueryValidationBehavior(
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper) :
    IPipelineBehavior<PickAssociateQuery, ErrorOr<AssociateModel>>
{
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));


    public async Task<ErrorOr<AssociateModel>> Handle(PickAssociateQuery request, 
        RequestHandlerDelegate<ErrorOr<AssociateModel>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        if (facade.WhereClauses.Any())
            return Error.Conflict("associate.invalid_combination", 
                "Filter(s) cannot be used select by id request.");
        
        var errors = new List<Error>();
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