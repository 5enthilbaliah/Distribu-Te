// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.Squads.DataContracts;
using Framework.OData;
using Microsoft.OData.ModelBuilder;

public class SquadResponseConfiguration : OdataResponseConfiguration<SquadResponse>
{
    public override void Configure(EntityTypeConfiguration<SquadResponse> typeConfiguration)
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