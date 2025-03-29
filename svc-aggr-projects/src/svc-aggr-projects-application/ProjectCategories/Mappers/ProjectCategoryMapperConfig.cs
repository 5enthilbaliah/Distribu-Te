namespace DistribuTe.Aggregates.Projects.Application.ProjectCategories.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;
using Shared;

public class ProjectCategoryMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectCategoryAggregate, ProjectCategoryModel>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Projects, src => src.Projects);
        
        config.NewConfig<ProjectCategoryAggregate, ProjectCategoryElement>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description);
    }
}