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
            .HasColumnName("name")
            .HasColumnType("varchar(45)");
        
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code")
            .HasColumnType("varchar(20)");
        
        builder.Property(p => p.Description)
            .HasMaxLength(200)
            .HasColumnName("description")
            .HasColumnType("varchar(200)");

        builder.Property(p => p.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(p => p.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("created_by")
            .HasColumnType("varchar(45)");
        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(45)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(45)");
        
        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id");
        
        builder.HasIndex(p => p.CategoryId);
    }
}

public class ProjectAggregateConfiguration : IEntityTypeConfiguration<ProjectAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectAggregate> builder)
    {
        var configuration = new ProjectConfiguration<ProjectAggregate>();
        configuration.Configure(builder);
        
        builder.HasOne(p => p.Category)
            .WithMany(p => p.Projects)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}