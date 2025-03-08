namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using DataContracts;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(ITeamsMutator<SquadAssociate, SquadAssociateId> mutator, 
    IUnitOfWork unitOfWork, IMapper mapper, ITeamsReader<SquadAssociate, SquadAssociateId> reader)
    : IRequestHandler<SpawnSquadAssociateCommand, SquadAssociateResponse>, 
        IRequestHandler<CommitSquadAssociateCommand, SquadAssociateResponse>,
        IRequestHandler<TrashSquadAssociateCommand, bool>
{
    private readonly ITeamsMutator<SquadAssociate, SquadAssociateId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ITeamsReader<SquadAssociate, SquadAssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    public async Task<SquadAssociateResponse> Handle(SpawnSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SquadAssociate>(request.SquadAssociate);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadAssociateResponse>(entity);
    }
    
    public async Task<SquadAssociateResponse> Handle(CommitSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var associateId = new AssociateId(request.AssociateId);
        var squadId = new SquadId(request.SquadId);
        var squadAssociateId = new SquadAssociateId(squadId, associateId);
        var change = _mapper.Map<SquadAssociate>(request.SquadAssociate);
        change.AssociateId = associateId;
        change.SquadId = squadId;
        change.Id = squadAssociateId;
        
        var toMutate = await _reader.PickAsync(squadAssociateId, cancellationToken);
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate!);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadAssociateResponse>(change);
    }

    public async Task<bool> Handle(TrashSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.SquadId);
        var associateId = new AssociateId(request.AssociateId);
        var squadAssociateId = new SquadAssociateId(squadId, associateId);
        
        var existing = await _reader.PickAsync(squadAssociateId, cancellationToken);
        _mutator.TrashOne(existing!);

        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}