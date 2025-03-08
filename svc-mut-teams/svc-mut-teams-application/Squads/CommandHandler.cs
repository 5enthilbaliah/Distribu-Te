namespace DistribuTe.Mutators.Teams.Application.Squads;

using DataContracts;
using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(ITeamsMutator<Squad, SquadId> mutator, 
    IUnitOfWork unitOfWork, IMapper mapper, ITeamsReader<Squad, SquadId> reader)
    : IRequestHandler<SpawnSquadCommand, SquadResponse>,
        IRequestHandler<CommitSquadCommand, SquadResponse>,
        IRequestHandler<TrashSquadCommand, bool>
{
    private readonly ITeamsMutator<Squad, SquadId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ITeamsReader<Squad, SquadId> _reader = 
        reader ?? throw new ArgumentNullException(nameof(reader));


    public async Task<SquadResponse> Handle(SpawnSquadCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Squad>(request.Squad);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadResponse>(entity);
    }

    public async Task<SquadResponse> Handle(CommitSquadCommand request, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.Id);
        var change = _mapper.Map<Squad>(request.Squad);
        change.Id = squadId;
        
        var toMutate = await _reader.PickAsync(squadId, cancellationToken);
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate!);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadResponse>(change);
    }

    public async Task<bool> Handle(TrashSquadCommand request, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.Id);
        
        var existing = await _reader.PickAsync(squadId, cancellationToken);
        _mutator.TrashOne(existing!);
        
        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}