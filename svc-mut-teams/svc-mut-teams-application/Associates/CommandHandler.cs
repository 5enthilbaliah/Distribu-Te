namespace DistribuTe.Mutators.Teams.Application.Associates;

using DataContracts;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(ITeamsMutator<Associate, AssociateId> mutator, 
    IUnitOfWork unitOfWork, IMapper mapper, ITeamsReader<Associate, AssociateId> reader)
    : IRequestHandler<SpawnAssociateCommand, AssociateResponse>,
        IRequestHandler<CommitAssociateCommand, AssociateResponse>,
        IRequestHandler<TrashAssociateCommand, bool>
{
    private readonly ITeamsMutator<Associate, AssociateId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ITeamsReader<Associate, AssociateId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));
    public async Task<AssociateResponse> Handle(SpawnAssociateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Associate>(request.Associate);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<AssociateResponse>(entity);
    }

    public async Task<AssociateResponse> Handle(CommitAssociateCommand request, CancellationToken cancellationToken)
    {
        var associateId = new AssociateId(request.Id);
        var change = _mapper.Map<Associate>(request.Associate);
        change.Id = associateId;
        
        var toMutate = await _reader.PickAsync(associateId, cancellationToken);
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate!);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<AssociateResponse>(change);
    }

    public async Task<bool> Handle(TrashAssociateCommand request, CancellationToken cancellationToken)
    {
        var associateId = new AssociateId(request.Id);
        
        var existing = await _reader.PickAsync(associateId, cancellationToken);
        _mutator.TrashOne(existing!);

        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}