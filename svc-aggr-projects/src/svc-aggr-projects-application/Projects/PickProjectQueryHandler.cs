namespace DistribuTe.Aggregates.Projects.Application.Projects;

using _base;
using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class PickProjectQueryHandler(
    IAggregateReader<ProjectAggregate, ProjectId> reader,
    EntityLinqMapper<SquadProjectAggregate, SquadProjectId> squadSubMapper,
    IMapper mapper) : ProjectQueryHandler(squadSubMapper),
    IRequestHandler<PickProjectQuery, ErrorOr<ProjectModel>>
{
    private readonly IAggregateReader<ProjectAggregate, ProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<ProjectModel>> Handle(PickProjectQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<ProjectAggregate>, IQueryable<ProjectAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var entity = await _reader.PickAsync(new ProjectId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Projects.NotFound;
        
        return _mapper.Map<ProjectModel>(entity);
    }
}