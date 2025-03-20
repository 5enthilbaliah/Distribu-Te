namespace DistribuTe.Aggregates.Teams.Application.Associates;

using Base;
using DataContracts;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using Framework.AppEssentials.Linq;
using MapsterMapper;
using MediatR;

public class PickAssociateAssociateQueryHandler(
    ITeamsReader<AssociateAggregate, AssociateId> reader,
    EntityLinqMapper<SquadAssociateAggregate, SquadAssociateId> squadSubMapper,
    IMapper mapper) : AssociateQueryHandler(squadSubMapper),
    IRequestHandler<PickAssociateQuery, ErrorOr<AssociateModel>>
{
    private readonly ITeamsReader<AssociateAggregate, AssociateId> _reader =
        reader ?? throw new ArgumentNullException(nameof(reader));
    
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<ErrorOr<AssociateModel>> Handle(PickAssociateQuery request, CancellationToken cancellationToken)
    {
        Func<IQueryable<AssociateAggregate>, IQueryable<AssociateAggregate>>? expander = null;
        if (request.EntityLinqFacade.InnerWhereClauses.Count != 0)
        {
            expander = FindExpander(request.EntityLinqFacade);
        }
        
        var entity = await _reader.PickAsync(new AssociateId(request.Id), expander: expander,
            cancellationToken: cancellationToken);

        if (entity == null)
            return Errors.Associates.NotFound;
        
        return _mapper.Map<AssociateModel>(entity);
    }
}