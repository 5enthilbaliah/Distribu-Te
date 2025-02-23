namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SquadConfiguration<TSquad> : IEntityTypeConfiguration<TSquad>
    where TSquad : Squad
{
    public void Configure(EntityTypeBuilder<TSquad> builder)
    {
        builder.ToTable("squads");

        builder.HasKey(s => s.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
   

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name");
        
        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code");
        
        builder.Property(s => s.Description)
            .HasMaxLength(200)
            .HasColumnName("description");

        builder.Property(s => s.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(s => s.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(s => s.CreatedBy)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("created_by");
        builder.Property(s => s.ModifiedBy)
            .HasMaxLength(200)
            .HasColumnName("modified_by");
    }
}

public class SquadAggregateConfiguration : IEntityTypeConfiguration<SquadAggregate>
{
    public void Configure(EntityTypeBuilder<SquadAggregate> builder)
    {
        var configuration = new SquadConfiguration<SquadAggregate>();
        configuration.Configure(builder);
    }
}