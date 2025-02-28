namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentItemTaskConfiguration<TDepTask> : IEntityTypeConfiguration<TDepTask>
    where TDepTask : BaseDeploymentItemTask
{
    public void Configure(EntityTypeBuilder<TDepTask> builder)
    {
        builder.ToTable("deployment_item_tasks");

        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        builder.Property(d => d.DeploymentItemId)
            .HasColumnName("deployment_item_id");
        builder.Property(d => d.AssociateId)
            .HasColumnName("associate_id");
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

        builder.Property(d => d.TaskTypeId)
            .HasColumnName("task_type_id");
    }
}

public class DeploymentItemTaskAggregateConfiguration : IEntityTypeConfiguration<DeploymentItemTaskAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentItemTaskAggregate> builder)
    {
        var configuration = new DeploymentItemTaskConfiguration<DeploymentItemTaskAggregate>();
        configuration.Configure(builder);
        
        builder.HasOne(d => d.Status)
            .WithMany(d => d.DeploymentItemTasks)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.StatusId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(d => d.Associate)
            .WithMany(d => d.DeploymentItemTasks)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.AssociateId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(d => d.DeploymentItem)
            .WithMany(d => d.DeploymentItemTasks)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.DeploymentItemId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(d => d.TaskType)
            .WithMany(d => d.DeploymentItemTasks)
            .HasPrincipalKey(d => d.Id)
            .HasForeignKey(d => d.TaskTypeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}