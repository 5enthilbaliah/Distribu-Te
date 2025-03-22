namespace DistribuTe.Domain.DeploymentEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BaseDeploymentItemConfiguration<TDepItem> : IEntityTypeConfiguration<TDepItem>
    where TDepItem : BaseDeploymentItem
{
    public void Configure(EntityTypeBuilder<TDepItem> builder)
    {
        builder.ToTable("deployment_items");

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