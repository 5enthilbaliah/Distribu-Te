namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SquadAggregateConfiguration : IEntityTypeConfiguration<SquadAggregate>
{
    public void Configure(EntityTypeBuilder<SquadAggregate> builder)
    {
        builder.ToTable("squads");

        builder.HasKey(s => s.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(45);
        
        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(s => s.Description)
            .HasMaxLength(200);

        builder.Property(s => s.CreatedOn)
            .HasColumnType("datetime2(7)");
        builder.Property(s => s.ModifiedOn)
            .HasColumnType("datetime2(7)");

        builder.Property(s => s.CreatedBy)
            .IsRequired()
            .HasMaxLength(200);
        builder.Property(s => s.ModifiedBy)
            .HasMaxLength(200);
    }
}