// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Projects.Apis.Odata;

using Application.ProjectCategories.DataContracts;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class ProjectCategoryResponseConfiguration : OdataResponseConfiguration<ProjectCategoryResponse>
{
    public override void Configure(EntityTypeConfiguration<ProjectCategoryResponse> typeConfiguration)
    {
        typeConfiguration.Property(a => a.Id)
            .Name = "id";
        
        typeConfiguration.Property(a => a.Name)
            .Name = "name";
        
        typeConfiguration.Property(a => a.Code)
            .Name = "code";
        
        typeConfiguration.Property(a => a.Description)
            .Name = "description";
    }
}