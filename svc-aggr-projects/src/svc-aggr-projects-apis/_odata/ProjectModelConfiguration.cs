// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Projects.Apis.Odata;

using Application.Projects.DataContracts;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class ProjectModelConfiguration : OdataResponseConfiguration<ProjectModel>
{
    public override void Configure(EntityTypeConfiguration<ProjectModel> typeConfiguration)
    {
        typeConfiguration.Property(a => a.Id)
            .Name = "id";
        
        typeConfiguration.Property(a => a.Category_Id)
            .Name = "category_id";
        
        typeConfiguration.Property(a => a.Name)
            .Name = "name";
        
        typeConfiguration.Property(a => a.Code)
            .Name = "code";
        
        typeConfiguration.Property(a => a.Description)
            .Name = "description";
        
        typeConfiguration.HasOptional(a => a.Category)
            .Name = "category";
    }
}