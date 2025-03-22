namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectEntities.Specifications;

public class SquadProjectAggregateConfiguration : IEntityTypeConfiguration<SquadProjectAggregate>
{
    public void Configure(EntityTypeBuilder<SquadProjectAggregate> builder)
    {
        builder.HasKey(s => new { s.SquadId, s.ProjectId });

        builder.Property(s => s.ProjectId)
            .HasColumnName("project_id");
        
        builder.Property(s => s.SquadId)
            .HasColumnName("squad_id");
        
        var configuration = new BaseSquadProjectConfiguration<SquadProjectAggregate>();
        configuration.Configure(builder);
        
        builder.HasOne(p => p.Project)
            .WithMany(p => p.SquadProjects)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(p => p.Squad)
            .WithMany(p => p.SquadProjects)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.SquadId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}