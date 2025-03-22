#pragma warning disable CS8603 // Possible null reference return.
namespace DistribuTe.Mutators.Projects.Application.Projects.Mappers;

using DataContracts;
using Domain.Entities;
using Mapster;

public class ProjectMapperConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ProjectRequest, Project>()
            .Ignore(dest => dest.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.CategoryId, src => new ProjectCategoryId(src.Category_Id))
            .Map(dest => dest.Description, src => src.Description)
            .Ignore(dest => dest.CreatedOn)
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.ModifiedOn)
            .Ignore(dest => dest.ModifiedBy);;

        config.NewConfig<Project, ProjectResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Category_Id, src => src.CategoryId.Value)
            .Map(dest => dest.Description, src => src.Description);
        
        config.NewConfig<Project, Project>()
            .Ignore(dest => dest.CreatedBy)
            .Ignore(dest => dest.CreatedOn);
    }
}