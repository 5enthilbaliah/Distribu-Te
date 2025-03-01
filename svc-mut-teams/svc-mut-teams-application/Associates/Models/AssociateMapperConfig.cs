namespace DistribuTe.Mutators.Teams.Application.Associates.Models;

using DistribuTe.Mutators.Teams.Domain.Entities;
using Mapster;

public class AssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssociateRm, Associate>()
            .Ignore(dest => dest.Id);
        
        config.NewConfig<Associate, AssociateVm>()
            .Map(dest => dest.Id, src => src.Id.Value);
    }
}