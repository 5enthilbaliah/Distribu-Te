namespace DistribuTe.Mutators.Teams.Application.Squads;

using Domain.Entities;
using Mapster;
using MapsterMapper;
using MediatR;
using Models;
using Shared;

public class CommandHandler(ITeamsRepository<Squad, SquadId> repository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnSquadCommand, SquadVm>,
        IRequestHandler<CommitSquadCommand, SquadVm>,
        IRequestHandler<TrashSquadCommand, bool>
{
    private readonly ITeamsRepository<Squad, SquadId> _repository =
        repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));


    public async Task<SquadVm> Handle(SpawnSquadCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Squad>(request.Squad);
        
        _repository.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadVm>(entity);
    }

    public async Task<SquadVm> Handle(CommitSquadCommand request, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.Id);
        var change = _mapper.Map<Squad>(request.Squad);
        change.Id = squadId;
        
        await _repository.CommitOneAsync(change, update =>  change.Adapt(update),
            cancellationToken);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadVm>(change);
    }

    public async Task<bool> Handle(TrashSquadCommand request, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.Id);
        
        await _repository.TrashOneAsync(squadId, cancellationToken);
        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}