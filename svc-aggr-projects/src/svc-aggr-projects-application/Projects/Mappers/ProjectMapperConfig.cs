namespace DistribuTe.Aggregates.Projects.Application.Projects.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;
using Shared;

public class ProjectMapperConfig: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectAggregate, ProjectModel>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Category_Id, src => src.CategoryId.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Category, src => src.Category)
            .Map(dest => dest.Squad_Projects, src => src.SquadProjects);
        
        config.NewConfig<ProjectAggregate, ProjectElement>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Category_Id, src => src.CategoryId.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description);
    }
}