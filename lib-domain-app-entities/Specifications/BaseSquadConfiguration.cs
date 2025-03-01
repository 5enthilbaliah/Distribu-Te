namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BaseSquadConfiguration<TSquad> : IEntityTypeConfiguration<TSquad>
    where TSquad : BaseSquad
{
    public void Configure(EntityTypeBuilder<TSquad> builder)
    {
        builder.ToTable("squads");

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");
        
        builder.Property(s => s.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code")
            .HasColumnType("varchar(20)");
        
        builder.HasIndex(a => a.Code)
            .IsUnique();
        builder.HasIndex(a => a.Name)
            .IsUnique();
        
        builder.Property(s => s.Description)
            .HasMaxLength(200)
            .HasColumnName("description")
            .HasColumnType("varchar(200)");

        builder.Property(s => s.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(s => s.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(s => s.CreatedBy)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("created_by")
            .HasColumnType("varchar(200)");
        builder.Property(s => s.ModifiedBy)
            .HasMaxLength(200)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(200)");
    }
}

public class SquadAggregateConfiguration : IEntityTypeConfiguration<SquadAggregate>
{
    public void Configure(EntityTypeBuilder<SquadAggregate> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseSquadConfiguration<SquadAggregate>();
        configuration.Configure(builder);
    }
}