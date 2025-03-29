namespace DistribuTe.Aggregates.Projects.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.ProjectEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ProjectCategoryAggregateConfiguration : IEntityTypeConfiguration<ProjectCategoryAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectCategoryAggregate> builder)
    {
        var converter = new ValueConverter<ProjectCategoryId, int>(
            id => id.Value, 
            value => new ProjectCategoryId(value));
        
        builder.HasKey(a => a.Id);
            
        builder.Property(a => a.Id)
            .HasConversion(converter)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        var configuration = new BaseProjectCategoryConfiguration<ProjectCategoryAggregate>();
        configuration.Configure(builder);
    }
}