namespace DistribuTe.Domain.AppEntities.Specifications;

using DeploymentEntities.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentAggregateConfiguration : IEntityTypeConfiguration<DeploymentAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentAggregate> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseDeploymentConfiguration<DeploymentAggregate>();
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