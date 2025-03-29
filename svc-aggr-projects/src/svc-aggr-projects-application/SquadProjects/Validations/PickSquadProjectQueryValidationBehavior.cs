namespace DistribuTe.Aggregates.Projects.Application.SquadProjects.Validations;

using DataContracts;
using ErrorOr;
using MediatR;

public class PickSquadProjectQueryValidationBehavior :
    IPipelineBehavior<PickSquadProjectQuery, ErrorOr<SquadProjectModel>>
{
    public async Task<ErrorOr<SquadProjectModel>> Handle(PickSquadProjectQuery request, 
        RequestHandlerDelegate<ErrorOr<SquadProjectModel>> next, CancellationToken cancellationToken)
    {
        var facade = request.EntityLinqFacade;
        if (facade.WhereClauses.Any())
            return Error.Conflict("query_squad_project.invalid_combination", 
                "Filter(s) cannot be used select by id request.");
        
        var response = await next();
        return response;
    }
}