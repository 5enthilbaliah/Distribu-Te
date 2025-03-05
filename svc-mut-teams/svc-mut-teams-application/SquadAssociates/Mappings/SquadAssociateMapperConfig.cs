namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Mappings;

using DataContracts;
using Domain.Entities;
using Mapster;

public class SquadAssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadAssociateRequest, SquadAssociate>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.AssociateId, src => new AssociateId(src.AssociateId))
            .Map(dest => dest.SquadId, src => new SquadId(src.SquadId));
        
        config.NewConfig<SquadAssociate, SquadAssociateResponse>()
            .Map(dest => dest.AssociateId, src => src.AssociateId.Value)
            .Map(dest => dest.SquadId, src => src.SquadId.Value);
        
        config.NewConfig<SquadAssociate, SquadAssociate>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}