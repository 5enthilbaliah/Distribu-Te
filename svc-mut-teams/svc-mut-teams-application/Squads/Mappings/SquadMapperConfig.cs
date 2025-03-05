namespace DistribuTe.Mutators.Teams.Application.Squads.Mappings;

using DataContracts;
using Domain.Entities;
using Mapster;

public class SquadMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadRequest, Squad>()
            .Ignore(dest => dest.Id);
        
        config.NewConfig<Squad, SquadResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
        
        config.NewConfig<Squad, Squad>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}