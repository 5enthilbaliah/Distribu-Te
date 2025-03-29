namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories;

using _base;
using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class PickSquadCategoryQueryHandler(
    IAggregateReader<ProjectCategoryAggregate, ProjectCategoryId> reader,
    EntityLinqMapper<ProjectAggregate, ProjectId> projectSubMapper,
    IMapper mapper) : ProjectCategoryQueryHandler(projectSubMapper),
    IRequestHandler<PickProjectCategoryQuery, ErrorOr<ProjectCategoryModel>>
{
    private readonly IAggregateReader<ProjectCategoryAggregate, ProjectCategoryId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<ProjectCategoryModel>> Handle(PickProjectCategoryQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<ProjectCategoryAggregate>, IQueryable<ProjectCategoryAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var entity = await _reader.PickAsync(new ProjectCategoryId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.ProjectCategories.NotFound;
        
        return _mapper.Map<ProjectCategoryModel>(entity);
    }
}