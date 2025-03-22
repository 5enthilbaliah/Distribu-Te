namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectEntities.Specifications;

public class ProjectCategoryAggregateConfiguration : IEntityTypeConfiguration<ProjectCategoryAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectCategoryAggregate> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseProjectCategoryConfiguration<ProjectCategoryAggregate>();
        configuration.Configure(builder);
    }
}