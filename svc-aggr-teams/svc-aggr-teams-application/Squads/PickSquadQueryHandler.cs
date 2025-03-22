namespace DistribuTe.Aggregates.Teams.Application.Squads;

using Base;
using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class PickSquadQueryHandler(
    IAggregateReader<SquadAggregate, SquadId> reader,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) : SquadQueryHandler(squadSubMapper),
    IRequestHandler<PickSquadQuery, ErrorOr<SquadModel>>
{
    private readonly IAggregateReader<SquadAggregate, SquadId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<SquadModel>> Handle(PickSquadQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<SquadAggregate>, IQueryable<SquadAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var entity = await _reader.PickAsync(new SquadId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Squads.NotFound;
        
        return _mapper.Map<SquadModel>(entity);
    }
}