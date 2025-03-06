namespace DistribuTe.Domain.AppEntities.Specifications;

using DeploymentEntities.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentItemAggregateConfiguration : IEntityTypeConfiguration<DeploymentItemAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentItemAggregate> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseDeploymentItemConfiguration<DeploymentItemAggregate>();
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