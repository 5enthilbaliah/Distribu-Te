namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Models;

public class CommandHandler(ITeamsRepository<Associate, AssociateId> repository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnAssociateCommand, AssociateVm>,
        IRequestHandler<CommitAssociateCommand, AssociateVm>,
        IRequestHandler<TrashAssociateCommand, bool>
{
    private readonly ITeamsRepository<Associate, AssociateId> _repository =
        repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<AssociateVm> Handle(SpawnAssociateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Associate>(request.Associate);
        _repository.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AssociateVm>(entity);
    }

    public async Task<AssociateVm> Handle(CommitAssociateCommand request, CancellationToken cancellationToken)
    {
        var associateId = new AssociateId(request.Id);
        var entity = _mapper.Map<Associate>(request.Associate);
        entity.Id = associateId;
        _repository.CommitOne(associateId, entity);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return _mapper.Map<AssociateVm>(entity);
    }

    public async Task<bool> Handle(TrashAssociateCommand request, CancellationToken cancellationToken)
    {
        var associateId = new AssociateId(request.Id);
        _repository.TrashOne(associateId);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}