// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Projects.Apis.Odata;

using Application.ProjectCategories.DataContracts;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class ProjectCategoryModelConfiguration : OdataResponseConfiguration<ProjectCategoryModel>
{
    public override void Configure(EntityTypeConfiguration<ProjectCategoryModel> typeConfiguration)
    {
        typeConfiguration.Property(a => a.Id)
            .Name = "id";
        
        typeConfiguration.Property(a => a.Name)
            .Name = "name";
        
        typeConfiguration.Property(a => a.Code)
            .Name = "code";
        
        typeConfiguration.Property(a => a.Description)
            .Name = "description";
        
        typeConfiguration.HasMany(a => a.Projects)
            .Name = "projects";
    }
}