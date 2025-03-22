#pragma warning disable CS8603 // Possible null reference return.
namespace DistribuTe.Mutators.Projects.Application.SquadProjects.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;

public class SquadProjectMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadProjectRequest, SquadProject>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.ProjectId, src => new ProjectId(src.Project_Id))
            .Map(dest => dest.SquadId, src => new SquadId(src.Squad_Id))
            .Map(dest => dest.StartedOn, src => src.Started_On)
            .Map(dest => dest.EndedOn, src => src.Ended_On)
            .Ignore(dest => dest.CreatedOn)
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.ModifiedOn)
            .Ignore(dest => dest.ModifiedBy);;

        config.NewConfig<SquadProject, SquadProjectResponse>()
            .Map(dest => dest.Project_Id, src => src.ProjectId.Value)
            .Map(dest => dest.Squad_Id, src => src.SquadId.Value)
            .Map(dest => dest.Started_On, src => src.StartedOn)
            .Map(dest => dest.Ended_On, src => src.EndedOn);
        
        config.NewConfig<SquadProject, SquadProject>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}