namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentConfiguration<TDeploy> : IEntityTypeConfiguration<TDeploy>
    where TDeploy : BaseDeployment
{
    public void Configure(EntityTypeBuilder<TDeploy> builder)
    {
        builder.ToTable("deployments");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");

        builder.Property(d => d.SquadId)
            .HasColumnName("squad_id");
        builder.Property(d => d.EnvironmentId)
            .HasColumnName("environment_id");
        
        builder.Property(d => d.PlannedStart)
            .HasColumnType("datetime2(7)")
            .HasColumnName("planned_start");
        builder.Property(d => d.PlannedEnd)
            .HasColumnType("datetime2(7)")
            .HasColumnName("planned_end");
        
        builder.Property(d => d.ActualStart)
            .HasColumnType("datetime2(7)")
            .HasColumnName("actual_start");
        builder.Property(d => d.ActualEnd)
            .HasColumnType("datetime2(7)")
            .HasColumnName("actual_end");
        
        builder.Property(d => d.Comments)
            .IsRequired()
            .HasMaxLength(2000)
            .HasColumnName("comments");
        
        builder.Property(d => d.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(d => d.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(d => d.CreatedBy)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("created_by")
            .HasColumnType("varchar(45)");
        builder.Property(d => d.ModifiedBy)
            .HasMaxLength(45)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(45)");
        
        builder.Property(d => d.StatusId)
            .HasColumnName("status_id");
    }
}

public class DeploymentAggregateConfiguration : IEntityTypeConfiguration<DeploymentAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentAggregate> builder)
    {
        var configuration = new DeploymentConfiguration<DeploymentAggregate>();
        configuration.Configure(builder);
        
        builder.HasOne(d => d.Status)
            .WithMany(d => d.Deployments)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.StatusId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(d => d.Environment)
            .WithMany(d => d.Deployments)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.EnvironmentId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(d => d.Squad)
            .WithMany(d => d.Deployments)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.SquadId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}