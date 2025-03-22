// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.Shared;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class SquadAssociateElementConfiguration : OdataResponseConfiguration<SquadAssociateElement>
{
    public override void Configure(EntityTypeConfiguration<SquadAssociateElement> typeConfiguration)
    {
        typeConfiguration.HasKey(x => new { SquadId = x.Squad_Id, AssociateId = x.Associate_Id });
        typeConfiguration.Property(a => a.Squad_Id)
            .Name = "squad_id";
        
        typeConfiguration.Property(a => a.Associate_Id)
            .Name = "associate_id";
        
        typeConfiguration.Property(a => a.Started_On)
            .Name = "started_on";

        typeConfiguration.Property(a => a.Ended_On)
            .Name = "ended_on";

        typeConfiguration.Property(a => a.Capacity)
            .Name = "capacity";
    }
}