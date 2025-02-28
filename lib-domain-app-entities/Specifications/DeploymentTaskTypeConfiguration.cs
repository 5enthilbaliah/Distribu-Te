namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeploymentTaskTypeConfiguration<TTaskType> : IEntityTypeConfiguration<TTaskType>
    where TTaskType : BaseDeploymentTaskType
{
    public void Configure(EntityTypeBuilder<TTaskType> builder)
    {
        builder.ToTable("deployment_task_types");

        builder.HasKey(d => d.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
   

        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");
        
        builder.Property(d => d.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code")
            .HasColumnType("varchar(20)");

        builder.Property(d => d.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(d => d.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(d => d.CreatedBy)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("created_by")
            .HasColumnType("varchar(200)");
        builder.Property(d => d.ModifiedBy)
            .HasMaxLength(200)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(200)");
    }
}

public class DeploymentTaskTypeAggregateConfiguration : IEntityTypeConfiguration<DeploymentTaskTypeAggregate>
{
    public void Configure(EntityTypeBuilder<DeploymentTaskTypeAggregate> builder)
    {
        var configuration = new DeploymentTaskTypeConfiguration<DeploymentTaskTypeAggregate>();
        configuration.Configure(builder);
    }
}