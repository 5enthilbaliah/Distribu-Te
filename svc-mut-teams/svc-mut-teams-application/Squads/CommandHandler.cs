namespace DistribuTe.Mutators.Teams.Application.Squads;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(ITeamsMutator<Squad, SquadId> mutator, 
    IExistingEntityMarker<Squad, SquadId> entityMarker, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnSquadCommand, ErrorOr<SquadResponse>>,
        IRequestHandler<CommitSquadCommand, ErrorOr<SquadResponse>>,
        IRequestHandler<TrashSquadCommand, ErrorOr<bool>>
{
    private readonly ITeamsMutator<Squad, SquadId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IExistingEntityMarker<Squad, SquadId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


    public async Task<ErrorOr<SquadResponse>> Handle(SpawnSquadCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Squad>(request.Squad);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadResponse>(entity);
    }

    public async Task<ErrorOr<SquadResponse>> Handle(CommitSquadCommand request, CancellationToken cancellationToken)
    {
        var change = _mapper.Map<Squad>(request.Squad);
        change.Id = _entityMarker.Id!;
        
        var toMutate = _entityMarker.Entity!;
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadResponse>(change);
    }

    public async Task<ErrorOr<bool>> Handle(TrashSquadCommand request, CancellationToken cancellationToken)
    {
        var existing = _entityMarker.Entity!;
        _mutator.TrashOne(existing);
        
        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}