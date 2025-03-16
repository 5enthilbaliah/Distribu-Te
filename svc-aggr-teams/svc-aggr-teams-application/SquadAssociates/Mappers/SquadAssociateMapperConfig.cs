namespace DistribuTe.Aggregates.Teams.Application.SquadAssociates.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;

public class SquadAssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadAssociateAggregate, SquadAssociateModel>()
            .Map(dest => dest.Associate_Id, src => src.AssociateId.Value)
            .Map(dest => dest.Squad_Id, src => src.SquadId.Value)
            .Map(dest => dest.Started_On, src => src.StartedOn)
            .Map(dest => dest.Ended_On, src => src.EndedOn)
            .Map(dest => dest.Capacity, src => src.Capacity);
    }
}