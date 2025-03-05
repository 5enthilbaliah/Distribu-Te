namespace DistribuTe.Mutators.Teams.Application.Associates.Mappings;

using DataContracts;
using Domain.Entities;
using Mapster;

public class AssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssociateRequest, Associate>()
            .Ignore(dest => dest.Id);
        
        config.NewConfig<Associate, AssociateResponse>()
            .Map(dest => dest.Id, src => src.Id.Value);
        
        config.NewConfig<Associate, Associate>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}