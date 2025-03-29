namespace DistribuTe.Aggregates.Projects.Application.SquadProjects.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;
using Shared;

public class SquadProjectMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SquadProjectAggregate, SquadProjectModel>()
            .Map(dest => dest.Project_Id, src => src.ProjectId.Value)
            .Map(dest => dest.Squad_Id, src => src.SquadId.Value)
            .Map(dest => dest.Started_On, src => src.StartedOn)
            .Map(dest => dest.Ended_On, src => src.EndedOn)
            .Map(dest => dest.Project, src => src.Project);
        
        config.NewConfig<SquadProjectAggregate, SquadProjectElement>()
            .Map(dest => dest.Project_Id, src => src.ProjectId.Value)
            .Map(dest => dest.Squad_Id, src => src.SquadId.Value)
            .Map(dest => dest.Started_On, src => src.StartedOn)
            .Map(dest => dest.Ended_On, src => src.EndedOn);
    }
}