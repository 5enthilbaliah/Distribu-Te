namespace DistribuTe.Mutators.Teams.Application.Associates;

using Domain;
using Domain.Entities;
using MediatR;

public class CommandHandler(ITeamsRepository<Associate, AssociateId> repository, IUnitOfWork unitOfWork)
    : IRequestHandler<SpawnAssociateCommand, AssociateId>,
        IRequestHandler<CommitAssociateCommand, bool>,
        IRequestHandler<TrashAssociateCommand, bool>
{
    private readonly ITeamsRepository<Associate, AssociateId> _repository =
        repository ?? throw new ArgumentNullException(nameof(repository));

    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));


    public async Task<AssociateId> Handle(SpawnAssociateCommand request, CancellationToken cancellationToken)
    {
        _repository.SpawnOne(request.Associate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return request.Associate.Id;
    }

    public async Task<bool> Handle(CommitAssociateCommand request, CancellationToken cancellationToken)
    {
        _repository.CommitOne(request.Id, request.Associate);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> Handle(TrashAssociateCommand request, CancellationToken cancellationToken)
    {
        _repository.TrashOne(request.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}