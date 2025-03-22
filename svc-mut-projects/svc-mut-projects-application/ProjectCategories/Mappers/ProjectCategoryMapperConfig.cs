#pragma warning disable CS8603 // Possible null reference return.
namespace DistribuTe.Mutators.Projects.Application.ProjectCategories.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;

public class ProjectCategoryMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectCategoryRequest, ProjectCategory>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description)
            .Ignore(dest => dest.CreatedOn)
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.ModifiedOn)
            .Ignore(dest => dest.ModifiedBy);;

        config.NewConfig<ProjectCategory, ProjectCategoryResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Description, src => src.Description);
        
        config.NewConfig<ProjectCategory, ProjectCategory>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}