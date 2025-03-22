namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.ProjectEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class SquadProjectConfiguration: IEntityTypeConfiguration<SquadProject>
{
    public void Configure(EntityTypeBuilder<SquadProject> builder)
    {
        var projectConverter = new ValueConverter<ProjectId, int>(
            id => id.Value, 
            value => new ProjectId(value));
        var squadConverter = new ValueConverter<SquadId, int>(
            id => id.Value, 
            value => new SquadId(value));

        builder.Ignore(c => c.Id);

        builder.HasKey(x => new { x.SquadId, x.ProjectId });
        builder.Property(x => x.SquadId)
            .HasColumnName("squad_id")
            .HasConversion(squadConverter);
        
        builder.Property(x => x.ProjectId)
            .HasColumnName("project_id")
            .HasConversion(projectConverter);
        
        var configuration = new BaseSquadProjectConfiguration<SquadProject>();
        configuration.Configure(builder);
    }
}