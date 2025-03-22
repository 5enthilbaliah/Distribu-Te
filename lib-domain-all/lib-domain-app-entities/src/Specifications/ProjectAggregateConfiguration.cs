namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectEntities.Specifications;

public class ProjectAggregateConfiguration : IEntityTypeConfiguration<ProjectAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectAggregate> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id");
        
        builder.HasIndex(p => p.CategoryId);
        
        var configuration = new BaseProjectConfiguration<ProjectAggregate>();
        configuration.Configure(builder);
        
        builder.HasOne(p => p.Category)
            .WithMany(p => p.Projects)
            .HasPrincipalKey(p => p.Id)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}