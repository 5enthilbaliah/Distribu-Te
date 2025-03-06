namespace DistribuTe.Domain.AppEntities.Specifications;

using DeploymentEntities.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnvironmentAggregateConfiguration : IEntityTypeConfiguration<EnvironmentAggregate>
{
    public void Configure(EntityTypeBuilder<EnvironmentAggregate> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseEnvironmentConfiguration<EnvironmentAggregate>();
        configuration.Configure(builder);
    }
}