namespace DistribuTe.Mutators.Teams.Application.Squads.Models;

using Domain.Entities;
using Mapster;

public class SquadMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadRm, Squad>()
            .Ignore(dest => dest.Id);
        
        config.NewConfig<Squad, SquadVm>()
            .Map(dest => dest.Id, src => src.Id.Value);
        
        config.NewConfig<Squad, Squad>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}