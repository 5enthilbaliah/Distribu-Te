namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProjectConfiguration<TProject> : IEntityTypeConfiguration<TProject>
    where TProject : Project
{
    public void Configure(EntityTypeBuilder<TProject> builder)
    {
        builder.ToTable("projects");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name");
        
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code");
        
        builder.Property(p => p.Description)
            .HasMaxLength(200)
            .HasColumnName("description");

        builder.Property(p => p.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(p => p.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("created_by");
        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(200)
            .HasColumnName("modified_by");
        
        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id");
        
        builder.HasIndex(p => p.CategoryId);
    }
}

public class ProjectConfiguration : IEntityTypeConfiguration<ProjectAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectAggregate> builder)
    {
        var configuration = new ProjectConfiguration<ProjectAggregate>();
        configuration.Configure(builder);
    }
}