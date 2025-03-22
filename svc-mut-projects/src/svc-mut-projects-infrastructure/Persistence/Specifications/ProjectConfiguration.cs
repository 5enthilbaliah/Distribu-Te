namespace DistribuTe.Mutators.Projects.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.ProjectEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class ProjectConfiguration: IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        var converter = new ValueConverter<ProjectId, int>(
            id => id.Value, 
            value => new ProjectId(value));
        
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id)
            .HasConversion(converter)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        var categoryConverter = new ValueConverter<ProjectCategoryId, int>(
            id => id.Value, 
            value => new ProjectCategoryId(value));
        builder.Property(a => a.CategoryId)
            .HasConversion(categoryConverter)
            .HasColumnName("category_id");
        
        var configuration = new BaseProjectConfiguration<Project>();
        configuration.Configure(builder);
    }
}