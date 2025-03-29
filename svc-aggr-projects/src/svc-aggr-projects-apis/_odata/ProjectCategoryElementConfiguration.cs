// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Projects.Apis.Odata;

using Application.Shared;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class ProjectCategoryElementConfiguration : OdataResponseConfiguration<ProjectCategoryElement>
{
    public override void Configure(EntityTypeConfiguration<ProjectCategoryElement> typeConfiguration)
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