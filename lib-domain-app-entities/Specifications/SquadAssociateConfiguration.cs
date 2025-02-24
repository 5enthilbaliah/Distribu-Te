namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SquadAssociateConfiguration<TSqdAssociate> : IEntityTypeConfiguration<TSqdAssociate>
    where TSqdAssociate : SquadAssociate
{
    public void Configure(EntityTypeBuilder<TSqdAssociate> builder)
    {
        builder.ToTable("squad_associates");

        builder.HasKey(s => new { s.SquadId, s.AssociateId });

        builder.Property(s => s.AssociateId)
            .HasColumnName("associate_id");
        
        builder.Property(s => s.SquadId)
            .HasColumnName("squad_id");
        
        builder.Property(s => s.StartedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("started_on");
        builder.Property(s => s.EndedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("ended_on");

        builder.Property(s => s.Capacity)
            .HasColumnName("capacity")
            .HasColumnType("decimal(5,4)");
        
        builder.Property(s => s.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(s => s.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(s => s.CreatedBy)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("created_by")
            .HasColumnType("varchar(45)");
        builder.Property(s => s.ModifiedBy)
            .HasMaxLength(45)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(45)");
    }
}

public class SquadAssociateAggregateConfiguration : IEntityTypeConfiguration<SquadAssociateAggregate>
{
    public void Configure(EntityTypeBuilder<SquadAssociateAggregate> builder)
    {
        var configuration = new SquadAssociateConfiguration<SquadAssociateAggregate>();
        configuration.Configure(builder);
        
        builder.HasOne(a => a.Associate)
            .WithMany(a => a.SquadAssociates)
            .HasForeignKey(a => a.AssociateId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(a => a.Squad)
            .WithMany(a => a.SquadAssociates)
            .HasForeignKey(a => a.SquadId)
            .HasPrincipalKey(a => a.Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}