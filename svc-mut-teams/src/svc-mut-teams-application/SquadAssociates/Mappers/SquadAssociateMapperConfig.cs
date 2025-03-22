#pragma warning disable CS8603 // Possible null reference return.
namespace DistribuTe.Mutators.Teams.Application.SquadAssociates.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;

public class SquadAssociateMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadAssociateRequest, SquadAssociate>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.AssociateId, src => new AssociateId(src.Associate_Id))
            .Map(dest => dest.SquadId, src => new SquadId(src.Squad_Id))
            .Map(dest => dest.StartedOn, src => src.Started_On)
            .Map(dest => dest.EndedOn, src => src.Ended_On)
            .Map(dest => dest.Capacity, src => src.Capacity)
            .Ignore(dest => dest.CreatedOn)
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.ModifiedOn)
            .Ignore(dest => dest.ModifiedBy);;
        
        config.NewConfig<SquadAssociate, SquadAssociateResponse>()
            .Map(dest => dest.Associate_Id, src => src.AssociateId.Value)
            .Map(dest => dest.Squad_Id, src => src.SquadId.Value)
            .Map(dest => dest.Started_On, src => src.StartedOn)
            .Map(dest => dest.Ended_On, src => src.EndedOn)
            .Map(dest => dest.Capacity, src => src.Capacity);
        
        config.NewConfig<SquadAssociate, SquadAssociate>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}