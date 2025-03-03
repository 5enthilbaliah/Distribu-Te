// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Teams.Apis.Odata;

using Application.Squads.Models;
using Framework.OData;
using Microsoft.OData.ModelBuilder;

public class SquadVmConfiguration : OdataVmConfiguration<SquadVm>
{
    public override void Configure(EntityTypeConfiguration<SquadVm> typeConfiguration)
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