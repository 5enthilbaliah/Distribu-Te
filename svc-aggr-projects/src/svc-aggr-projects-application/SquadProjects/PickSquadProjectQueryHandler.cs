namespace DistribuTe.Aggregates.Projects.Application.SquadProjects;

using _base;
using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class PickSquadProjectQueryHandler(
    IAggregateReader<SquadProjectAggregate, SquadProjectId> reader,
    IMapper mapper) : SquadProjectQueryHandler,
    IRequestHandler<PickSquadProjectQuery, ErrorOr<SquadProjectModel>>
{
    private readonly IAggregateReader<SquadProjectAggregate, SquadProjectId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<SquadProjectModel>> Handle(PickSquadProjectQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<SquadProjectAggregate>, IQueryable<SquadProjectAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var squadId = new SquadId(request.SquadId);
        var projectId = new ProjectId(request.ProjectId);
        var entity = await _reader.PickAsync(new SquadProjectId(squadId, projectId), 
            expander: expander, cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Projects.NotFound;
        
        return _mapper.Map<SquadProjectModel>(entity);
    }
}