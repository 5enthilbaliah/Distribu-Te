namespace DistribuTe.Domain.AppEntities.Specifications;

using DeploymentEntities.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentStatusAggregateConfiguration : IEntityTypeConfiguration<DeploymentStatusAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentStatusAggregate> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseDeploymentStatusConfiguration<DeploymentStatusAggregate>();
        configuration.Configure(builder);
    }
}
