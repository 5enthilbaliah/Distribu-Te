namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(ITeamsMutator<SquadAssociate, SquadAssociateId> mutator, 
    IExistingEntityMarker<SquadAssociate, SquadAssociateId> entityMarker, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnSquadAssociateCommand, ErrorOr<SquadAssociateResponse>>, 
        IRequestHandler<CommitSquadAssociateCommand, ErrorOr<SquadAssociateResponse>>,
        IRequestHandler<TrashSquadAssociateCommand, ErrorOr<bool>>
{
    private readonly ITeamsMutator<SquadAssociate, SquadAssociateId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IExistingEntityMarker<SquadAssociate, SquadAssociateId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<SquadAssociateResponse>> Handle(SpawnSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SquadAssociate>(request.SquadAssociate);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadAssociateResponse>(entity);
    }
    
    public async Task<ErrorOr<SquadAssociateResponse>> Handle(CommitSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var change = _mapper.Map<SquadAssociate>(request.SquadAssociate);
        change.AssociateId = _entityMarker.Id!.AssociateId;
        change.SquadId = _entityMarker.Id.SquadId;
        change.Id = _entityMarker.Id;
        
        var toMutate = _entityMarker.Entity!;
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate!);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadAssociateResponse>(change);
    }

    public async Task<ErrorOr<bool>> Handle(TrashSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var existing = _entityMarker.Entity!;
        _mutator.TrashOne(existing!);

        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}