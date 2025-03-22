// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.Shared;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class SquadElementConfiguration : OdataResponseConfiguration<SquadElement>
{
    public override void Configure(EntityTypeConfiguration<SquadElement> typeConfiguration)
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