// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.Squads;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class SquadModelConfiguration : OdataResponseConfiguration<SquadModel>
{
    public override void Configure(EntityTypeConfiguration<SquadModel> typeConfiguration)
    {
        typeConfiguration.Property(a => a.Id)
            .Name = "id";
        
        typeConfiguration.Property(a => a.Name)
            .Name = "name";
        
        typeConfiguration.Property(a => a.Code)
            .Name = "code";

        typeConfiguration.Property(a => a.Description)
            .Name = "description";
        
        typeConfiguration.HasMany(x => x.Squad_Associates)
            .Name = "squad_associates";
    }
}