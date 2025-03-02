namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Models;

using Domain.Entities;
using Mapster;

public class SquadAssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadAssociateRm, SquadAssociate>()
            .Ignore(dest => dest.Id);
        
        config.NewConfig<SquadAssociate, SquadAssociateVm>();
        
        config.NewConfig<SquadAssociate, SquadAssociate>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}