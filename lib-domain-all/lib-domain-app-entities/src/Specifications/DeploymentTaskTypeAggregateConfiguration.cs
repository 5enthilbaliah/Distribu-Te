namespace DistribuTe.Domain.AppEntities.Specifications;

using DeploymentEntities.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentTaskTypeAggregateConfiguration : IEntityTypeConfiguration<DeploymentTaskTypeAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentTaskTypeAggregate> builder)
    {
        builder.HasKey(d => d.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseDeploymentTaskTypeConfiguration<DeploymentTaskTypeAggregate>();
        configuration.Configure(builder);
    }
}