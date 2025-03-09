namespace DistribuTe.Mutators.Teams.Application.Associates;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Mapster;
using MapsterMapper;
using MediatR;
using Shared;

public class CommandHandler(ITeamsMutator<Associate, AssociateId> mutator, 
    IExistingEntityMarker<Associate, AssociateId> entityMarker, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnAssociateCommand, ErrorOr<AssociateResponse>>,
        IRequestHandler<CommitAssociateCommand, ErrorOr<AssociateResponse>>,
        IRequestHandler<TrashAssociateCommand, ErrorOr<bool>>
{
    private readonly ITeamsMutator<Associate, AssociateId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IExistingEntityMarker<Associate, AssociateId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<ErrorOr<AssociateResponse>> Handle(SpawnAssociateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Associate>(request.Associate);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<AssociateResponse>(entity);
    }

    public async Task<ErrorOr<AssociateResponse>> Handle(CommitAssociateCommand request, CancellationToken cancellationToken)
    {
        var change = _mapper.Map<Associate>(request.Associate);
        change.Id = _entityMarker.Id!;
        
        var toMutate = _entityMarker.Entity!;
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<AssociateResponse>(change);
    }

    public async Task<ErrorOr<bool>> Handle(TrashAssociateCommand request, CancellationToken cancellationToken)
    {
        var existing = _entityMarker.Entity!;
        _mutator.TrashOne(existing);

        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}