namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentStatusConfiguration<TStatus> : IEntityTypeConfiguration<TStatus>
    where TStatus : DeploymentStatus
{
    public void Configure(EntityTypeBuilder<TStatus> builder)
    {
        builder.ToTable("deployment_statuses");

        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
   

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(e => e.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("created_by")
            .HasColumnType("varchar(200)");
        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(200)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(200)");
    }
}

public class DeploymentStatusAggregateConfiguration : IEntityTypeConfiguration<DeploymentStatusAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentStatusAggregate> builder)
    {
        var configuration = new DeploymentStatusConfiguration<DeploymentStatusAggregate>();
        configuration.Configure(builder);
    }
}
