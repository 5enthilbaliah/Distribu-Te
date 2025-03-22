namespace DistribuTe.Mutators.Projects.Application.Projects;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(IEntityMutator<Project, ProjectId> mutator, 
    IExistingEntityMarker<Project, ProjectId> entityMarker, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnProjectCommand, ErrorOr<ProjectResponse>>,
        IRequestHandler<CommitProjectCommand, ErrorOr<ProjectResponse>>,
        IRequestHandler<TrashProjectCommand, ErrorOr<bool>>
{
    private readonly IEntityMutator<Project, ProjectId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IExistingEntityMarker<Project, ProjectId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<ErrorOr<ProjectResponse>> Handle(SpawnProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Project>(request.Project);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<ProjectResponse>(entity);
    }

    public async Task<ErrorOr<ProjectResponse>> Handle(CommitProjectCommand request, CancellationToken cancellationToken)
    {
        var change = _mapper.Map<Project>(request.Project);
        change.Id = _entityMarker.Id!;
        
        var toMutate = _entityMarker.Entity!;
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<ProjectResponse>(change);
    }

    public async Task<ErrorOr<bool>> Handle(TrashProjectCommand request, CancellationToken cancellationToken)
    {
        var existing = _entityMarker.Entity!;
        _mutator.TrashOne(existing);

        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return true;
    }
}