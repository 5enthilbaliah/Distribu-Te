namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProjectCategoryAggregateConfiguration : IEntityTypeConfiguration<ProjectCategoryAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectCategoryAggregate> builder)
    {
        builder.ToTable("project_categories");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(45);
        
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(p => p.Description)
            .HasMaxLength(200);

        builder.Property(p => p.CreatedOn)
            .HasColumnType("datetime2(7)");
        builder.Property(p => p.ModifiedOn)
            .HasColumnType("datetime2(7)");

        builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(200);

        builder.HasMany(p => p.Projects)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId)
            .HasPrincipalKey(p => p.Id);
    }
}