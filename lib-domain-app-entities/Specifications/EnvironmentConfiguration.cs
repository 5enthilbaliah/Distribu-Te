namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnvironmentConfiguration<TEnv> : IEntityTypeConfiguration<TEnv>
    where TEnv : Environment
{
    public void Configure(EntityTypeBuilder<TEnv> builder)
    {
        builder.ToTable("environments");

        builder.HasKey(e => e.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
   

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");
        
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code")
            .HasColumnType("varchar(20)");

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

public class EnvironmentAggregateConfiguration : IEntityTypeConfiguration<EnvironmentAggregate>
{
    public void Configure(EntityTypeBuilder<EnvironmentAggregate> builder)
    {
        var configuration = new EnvironmentConfiguration<EnvironmentAggregate>();
        configuration.Configure(builder);
        
        builder.HasMany(e => e.Deployments)
            .WithOne(e => e.Environment)
            .HasForeignKey(e => e.EnvironmentId)
            .HasPrincipalKey(e => e.Id);
    }
}