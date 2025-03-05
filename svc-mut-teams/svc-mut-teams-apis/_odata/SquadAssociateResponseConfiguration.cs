// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.SquadAssociates.DataContracts;
using Framework.OData;
using Microsoft.OData.ModelBuilder;

public class SquadAssociateResponseConfiguration : OdataResponseConfiguration<SquadAssociateResponse>
{
    public override void Configure(EntityTypeConfiguration<SquadAssociateResponse> typeConfiguration)
    {
        typeConfiguration.HasKey(x => new { x.SquadId, x.AssociateId });
        typeConfiguration.Property(a => a.SquadId)
            .Name = "squad_id";
        
        typeConfiguration.Property(a => a.AssociateId)
            .Name = "associate_id";
        
        typeConfiguration.Property(a => a.StartedOn)
            .Name = "started_on";

        typeConfiguration.Property(a => a.EndedOn)
            .Name = "ended_on";

        typeConfiguration.Property(a => a.Capacity)
            .Name = "capacity";
    }
}