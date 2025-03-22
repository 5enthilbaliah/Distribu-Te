namespace DistribuTe.Mutators.Projects.Application.ProjectCategories;

using DataContracts;
using Domain.Entities;
using ErrorOr;
using Framework.AppEssentials;
using Mapster;
using MapsterMapper;
using MediatR;

public class CommandHandler(IProjectsMutator<ProjectCategory, ProjectCategoryId> mutator, 
    IExistingEntityMarker<ProjectCategory, ProjectCategoryId> entityMarker, IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<SpawnProjectCategoryCommand, ErrorOr<ProjectCategoryResponse>>,
        IRequestHandler<CommitProjectCategoryCommand, ErrorOr<ProjectCategoryResponse>>,
        IRequestHandler<TrashProjectCategoryCommand, ErrorOr<bool>>
{
    private readonly IProjectsMutator<ProjectCategory, ProjectCategoryId> _mutator =
        mutator ?? throw new ArgumentNullException(nameof(mutator));
    private readonly IExistingEntityMarker<ProjectCategory, ProjectCategoryId> _entityMarker = 
        entityMarker ?? throw new ArgumentNullException(nameof(entityMarker));
    private readonly IUnitOfWork _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<ErrorOr<ProjectCategoryResponse>> Handle(SpawnProjectCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<ProjectCategory>(request.ProjectCategory);
        
        _mutator.SpawnOne(entity);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<ProjectCategoryResponse>(entity);
    }

    public async Task<ErrorOr<ProjectCategoryResponse>> Handle(CommitProjectCategoryCommand request, CancellationToken cancellationToken)
    {
        var change = _mapper.Map<ProjectCategory>(request.ProjectCategory);
        change.Id = _entityMarker.Id!;
        
        var toMutate = _entityMarker.Entity!;
        change.Adapt(toMutate);
        
        _mutator.CommitOne(toMutate);
        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return _mapper.Map<ProjectCategoryResponse>(change);
    }

    public async Task<ErrorOr<bool>> Handle(TrashProjectCategoryCommand request, CancellationToken cancellationToken)
    {
        var existing = _entityMarker.Entity!;
        _mutator.TrashOne(existing);

        await _unitOfWork.SaveChangesAsync(request.User!, cancellationToken);
        return true;
    }
}