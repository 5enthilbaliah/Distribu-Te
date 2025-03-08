// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.SquadAssociates.DataContracts;
using Framework.OData;
using Microsoft.OData.ModelBuilder;

public class SquadAssociateResponseConfiguration : OdataResponseConfiguration<SquadAssociateResponse>
{
    public override void Configure(EntityTypeConfiguration<SquadAssociateResponse> typeConfiguration)
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