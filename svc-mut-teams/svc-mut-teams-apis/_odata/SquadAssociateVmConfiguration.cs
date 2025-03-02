// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.SquadAssociates.Models;
using Microsoft.OData.ModelBuilder;

public class SquadAssociateVmConfiguration : OdataVmConfiguration<SquadAssociateVm>
{
    public override void Configure(EntityTypeConfiguration<SquadAssociateVm> typeConfiguration)
    {
        typeConfiguration.HasKey(x => new { x.SquadId, x.AssociateId });
    }
}