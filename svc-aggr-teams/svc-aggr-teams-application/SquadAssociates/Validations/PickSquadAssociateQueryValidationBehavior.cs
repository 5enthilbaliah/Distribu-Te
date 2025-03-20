namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates.Validations;

using DataContracts;
using ErrorOr;
using MediatR;

public class PickSquadAssociateQueryValidationBehavior :
    IPipelineBehavior<PickSquadAssociateQuery, ErrorOr<SquadAssociateModel>>
{
    public async Task<ErrorOr<SquadAssociateModel>> Handle(PickSquadAssociateQuery request, 
        RequestHandlerDelegate<ErrorOr<SquadAssociateModel>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        if (facade.WhereClauses.Count != 0)
            return Error.Conflict("squad_associate.invalid_combination", 
                "Filter(s) cannot be used select by id request.");
        
        var response = await next();
        return response;
    }
}