// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.SquadAssociates.Models;
using Microsoft.OData.ModelBuilder;

public class SquadAssociateVmConfiguration : OdataVmConfiguration<SquadAssociateVm>
{
    public override void Configure(EntityTypeConfiguration<SquadAssociateVm> typeConfiguration)
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