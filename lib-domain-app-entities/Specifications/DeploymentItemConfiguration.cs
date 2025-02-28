namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentItemConfiguration<TDepItem> : IEntityTypeConfiguration<TDepItem>
    where TDepItem : BaseDeploymentItem
{
    public void Configure(EntityTypeBuilder<TDepItem> builder)
    {
        builder.ToTable("deployment_items");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(d => d.DeploymentId)
            .HasColumnName("deployment_id");
        builder.Property(d => d.ProjectId)
            .HasColumnName("project_id");
        builder.Property(d => d.Sequence)
            .HasColumnName("sequence");
        
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

public class DeploymentItemAggregateConfiguration : IEntityTypeConfiguration<DeploymentItemAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentItemAggregate> builder)
    {
        var configuration = new DeploymentItemConfiguration<DeploymentItemAggregate>();
        configuration.Configure(builder);

        builder.HasOne(d => d.Status)
            .WithMany(d => d.DeploymentItems)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.StatusId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(d => d.Deployment)
            .WithMany(d => d.DeploymentItems)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.DeploymentId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(d => d.Project)
            .WithMany(d => d.DeploymentItems)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.ProjectId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}