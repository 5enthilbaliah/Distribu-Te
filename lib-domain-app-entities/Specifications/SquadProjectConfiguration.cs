namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SquadProjectConfiguration<TSqdProject> : IEntityTypeConfiguration<TSqdProject>
    where TSqdProject : BaseSquadProject
{
    public void Configure(EntityTypeBuilder<TSqdProject> builder)
    {
        builder.ToTable("squad_projects");
        
        builder.Property(s => s.StartedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("started_on");
        builder.Property(s => s.EndedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("ended_on");
        
        builder.Property(s => s.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(s => s.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(s => s.CreatedBy)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("created_by")
            .HasColumnType("varchar(45)");
        builder.Property(s => s.ModifiedBy)
            .HasMaxLength(45)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(45)");
    }
}

public class SquadProjectAggregateConfiguration : IEntityTypeConfiguration<SquadProjectAggregate>
{
    public void Configure(EntityTypeBuilder<SquadProjectAggregate> builder)
    {
        builder.HasKey(s => new { s.SquadId, s.ProjectId });

        builder.Property(s => s.ProjectId)
            .HasColumnName("project_id");
        
        builder.Property(s => s.SquadId)
            .HasColumnName("squad_id");
        
        var configuration = new SquadProjectConfiguration<SquadProjectAggregate>();
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