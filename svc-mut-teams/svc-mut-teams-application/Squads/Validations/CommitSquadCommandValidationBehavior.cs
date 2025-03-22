namespace DistribuTe.Mutators.Teams.Application.Squads.Validations;

using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using FluentValidation;
using Framework.AppEssentials;
using Framework.AppEssentials.Validations;
using MediatR;

public class CommitSquadCommandValidationBehavior(IEntityReader<Squad, SquadId> reader,
    IValidator<SquadRequest> validator, IExistingEntityMarker<Squad, SquadId> entityMarker) : 
    DistribuTeRequestValidationBehavior<SquadRequest>(validator),
    IPipelineBehavior<CommitSquadCommand, ErrorOr<SquadResponse>>
{
    private readonly IEntityReader<Squad, SquadId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IExistingEntityMarker<Squad, SquadId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    
    public async Task<ErrorOr<SquadResponse>> Handle(CommitSquadCommand request, RequestHandlerDelegate<ErrorOr<SquadResponse>> next, CancellationToken cancellationToken)
    {
        var (success, errors) = Validate(request.Squad);
        if (!success)
            return errors;
        
        var squadId = new SquadId(request.Id);
        var existing = await _reader.PickAsync(squadId, cancellationToken);

        if (existing is null)
            return Errors.Squads.NotFound;

        _entityMarker.Id = squadId;
        _entityMarker.Entity = existing;

        var response = await next();
        return response;
    }
}