// ReSharper disable once CheckNamespace
namespace DistribuTe.Mutators.Projects.Apis.Odata;

using Application.SquadProjects.DataContracts;
using Framework.ApiEssentials.Odata;
using Microsoft.OData.ModelBuilder;

public class SquadProjectResponseConfiguration : OdataResponseConfiguration<SquadProjectResponse>
{
    public override void Configure(EntityTypeConfiguration<SquadProjectResponse> typeConfiguration)
    {
        typeConfiguration.HasKey(x => new { SquadId = x.Squad_Id, ProjectId = x.Project_Id });
        
        typeConfiguration.Property(a => a.Started_On)
            .Name = "started_on";
        
        typeConfiguration.Property(a => a.Ended_On)
            .Name = "ended_on";
    }
}