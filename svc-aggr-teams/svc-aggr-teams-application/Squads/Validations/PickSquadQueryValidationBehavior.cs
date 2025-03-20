namespace DistribuTe.Aggregates.Teams.Application.Squads.Validations;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MediatR;

public class PickSquadQueryValidationBehavior(
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper) :
    IPipelineBehavior<PickSquadQuery, ErrorOr<SquadModel>>
{
    private readonly EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> _squadSubMapper =
        squadSubMapper ?? throw new ArgumentNullException(nameof(squadSubMapper));
    
    public async Task<ErrorOr<SquadModel>> Handle(PickSquadQuery request, 
        RequestHandlerDelegate<ErrorOr<SquadModel>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        if (facade.WhereClauses.Any())
            return Error.Conflict("query_squad.invalid_combination", 
                "Filter(s) cannot be used select by id request.");
        
        var errors = new List<Error>();
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