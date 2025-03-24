// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Projects.Apis.Odata;

using Application.Projects.DataContracts;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class ProjectResponseConfiguration : OdataResponseConfiguration<ProjectResponse>
{
    public override void Configure(EntityTypeConfiguration<ProjectResponse> typeConfiguration)
    {
        typeConfiguration.Property(a => a.Id)
            .Name = "id";
        
        typeConfiguration.Property(a => a.Name)
            .Name = "name";
        
        typeConfiguration.Property(a => a.Code)
            .Name = "code";
        
        typeConfiguration.Property(a => a.Category_Id)
            .Name = "category_id";
        
        typeConfiguration.Property(a => a.Description)
            .Name = "description";
    }
}