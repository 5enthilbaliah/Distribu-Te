namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates;

using Base;
using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials;
using MapsterMapper;
using MediatR;

public class PickSquadAssociateQueryHandler(IAggregateReader<SquadAssociateAggregate, SquadAssociateId> reader,
    IMapper mapper) : SquadAssociateQueryHandler,
    IRequestHandler<PickSquadAssociateQuery, ErrorOr<SquadAssociateModel>>
{
    private readonly IAggregateReader<SquadAssociateAggregate, SquadAssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<SquadAssociateModel>> Handle(PickSquadAssociateQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<SquadAssociateAggregate>, IQueryable<SquadAssociateAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var squadId = new SquadId(request.SquadId);
        var associateId = new AssociateId(request.AssociateId);
        var entity = await _reader.PickAsync(new SquadAssociateId(squadId, associateId), 
            expander: expander, cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.SquadAssociates.NotFound;
        
        return _mapper.Map<SquadAssociateModel>(entity);
    }
}