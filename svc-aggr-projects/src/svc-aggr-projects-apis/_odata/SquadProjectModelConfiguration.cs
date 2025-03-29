// ReSharper disable once CheckNamespace
namespace DistribuTe.Aggregates.Projects.Apis.Odata;

using Application.SquadProjects.DataContracts;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class SquadProjectModelConfiguration : OdataResponseConfiguration<SquadProjectModel>
{
    public override void Configure(EntityTypeConfiguration<SquadProjectModel> typeConfiguration)
    {
        typeConfiguration.HasKey(x => new { x.Squad_Id, x.Project_Id });
        typeConfiguration.Property(a => a.Squad_Id)
            .Name = "squad_id";
        
        typeConfiguration.Property(a => a.Project_Id)
            .Name = "project_id";
        
        typeConfiguration.Property(a => a.Started_On)
            .Name = "started_on";

        typeConfiguration.Property(a => a.Ended_On)
            .Name = "ended_on";

        typeConfiguration.HasOptional(x => x.Project)
            .Name = "project";
    }
}