// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Teams.Apis.Odata;

using Application.SquadAssociates;
using Framework.OData;
using Microsoft.OData.ModelBuilder;

public class SquadAssociateModelConfiguration : OdataResponseConfiguration<SquadAssociateModel>
{
    public override void Configure(EntityTypeConfiguration<SquadAssociateModel> typeConfiguration)
    {
        typeConfiguration.HasKey(x => new { x.Squad_Id, x.Associate_Id });
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
        
        // typeConfiguration.HasOptional(x => x.Squad)
        //     .Name = "squad";
        // typeConfiguration.HasOptional(x => x.Associate)
        //     .Name = "associate";
    }
}