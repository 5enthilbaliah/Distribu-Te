namespace DistribuTe.Aggregates.Teams.Application.Squads.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;
using Shared;

public class SquadMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadAggregate, SquadModel>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Squad_Associates, src => src.SquadAssociates);
        
        config.NewConfig<SquadAggregate, SquadElement>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description);
    }
}