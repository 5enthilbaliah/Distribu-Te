namespace DistribuTe.Mutators.Projects.Application.SquadProjects;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(
    IEntityMutator<SquadProject, SquadProjectId> mutator,
    IExistingEntityMarker<SquadProject, SquadProjectId> entityMarker,
    IUnitOfWork unitOfWork,
    IMapper mapper)
    : IRequestHandler<SpawnSquadProjectCommand, ErrorOr<SquadProjectResponse>>,
        IRequestHandler<CommitSquadProjectCommand, ErrorOr<SquadProjectResponse>>,
        IRequestHandler<TrashSquadProjectCommand, ErrorOr<bool>>
{
    private readonly IEntityMutator<SquadProject, SquadProjectId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IExistingEntityMarker<SquadProject, SquadProjectId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<SquadProjectResponse>> Handle(SpawnSquadProjectCommand request, 
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SquadProject>(request.SquadProject);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadProjectResponse>(entity);
    }

    public async Task<ErrorOr<SquadProjectResponse>> Handle(CommitSquadProjectCommand request, 
        CancellationToken cancellationToken)
    {
        var change = _mapper.Map<SquadProject>(request.SquadProject);
        change.ProjectId = _entityMarker.Id!.ProjectId;
        change.SquadId = _entityMarker.Id.SquadId;
        change.Id = _entityMarker.Id;
        
        var toMutate = _entityMarker.Entity!;
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate!);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<SquadProjectResponse>(change);
    }

    public async Task<ErrorOr<bool>> Handle(TrashSquadProjectCommand request, CancellationToken cancellationToken)
    {
        var existing = _entityMarker.Entity!;
        _mutator.TrashOne(existing!);

        await _unitOfWork.SaveChangesAsync("", cancellationToken);
        return true;
    }
}