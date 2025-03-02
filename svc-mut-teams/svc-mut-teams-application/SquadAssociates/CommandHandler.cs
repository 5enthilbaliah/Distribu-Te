namespace DistribuTe.Mutators.Teams.Application.SquadAssociates;

using Domain;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Models;

public class CommandHandler(ITeamsRepository<SquadAssociate, SquadAssociateId> repository, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnSquadAssociateCommand, SquadAssociateVm>, 
        IRequestHandler<TrashSquadAssociateCommand, bool>
{
    private readonly ITeamsRepository<SquadAssociate, SquadAssociateId> _repository =
        repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<SquadAssociateVm> Handle(SpawnSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SquadAssociate>(request.SquadAssociate);
        _repository.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadAssociateVm>(entity);
    }

    public async Task<bool> Handle(TrashSquadAssociateCommand request, CancellationToken cancellationToken)
    {
        var squadId = new SquadId(request.SquadId);
        var associateId = new AssociateId(request.AssociateId);
        var squadAssociateId = new SquadAssociateId(squadId, associateId);
        await _repository.TrashOneAsync(squadAssociateId, cancellationToken);
        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}