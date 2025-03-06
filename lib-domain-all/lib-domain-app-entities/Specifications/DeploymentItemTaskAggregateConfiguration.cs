namespace DistribuTe.Domain.AppEntities.Specifications;

using DeploymentEntities.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentItemTaskAggregateConfiguration : IEntityTypeConfiguration<DeploymentItemTaskAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentItemTaskAggregate> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseDeploymentItemTaskConfiguration<DeploymentItemTaskAggregate>();
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