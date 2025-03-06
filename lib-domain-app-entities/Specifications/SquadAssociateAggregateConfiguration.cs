namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamEntities.Specifications;

public class SquadAssociateAggregateConfiguration : IEntityTypeConfiguration<SquadAssociateAggregate>
{
    public void Configure(EntityTypeBuilder<SquadAssociateAggregate> builder)
    {
        builder.HasKey(s => new { s.SquadId, s.AssociateId });

        builder.Property(s => s.AssociateId)
            .HasColumnName("associate_id");
        
        builder.Property(s => s.SquadId)
            .HasColumnName("squad_id");
        
        var configuration = new BaseSquadAssociateConfiguration<SquadAssociateAggregate>();
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