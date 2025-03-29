namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.ProjectEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ProjectAggregateConfiguration : IEntityTypeConfiguration<ProjectAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectAggregate> builder)
    {
        var converter = new ValueConverter<ProjectId, int>(
            id => id.Value, 
            value => new ProjectId(value));
        
        var categoryConverter = new ValueConverter<ProjectCategoryId, int>(
            id => id.Value,
            value => new ProjectCategoryId(value));
        
        builder.HasKey(a => a.Id);
            
        builder.Property(a => a.Id)
            .HasConversion(converter)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.CategoryId)
            .HasConversion(categoryConverter)
            .HasColumnName("category_id");
        
        builder.HasIndex(p => p.CategoryId);
        
        var configuration = new BaseProjectConfiguration<ProjectAggregate>();
        configuration.Configure(builder);

        builder.HasOne(p => p.Category)
            .WithMany(p => p.Projects)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.CategoryId);
    }
}