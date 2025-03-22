namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.TeamEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class SquadAssociateAggregateConfiguration : IEntityTypeConfiguration<SquadAssociateAggregate>
{
    public void Configure(EntityTypeBuilder<SquadAssociateAggregate> builder)
    {
        var associateConverter = new ValueConverter<AssociateId, int>(
            id => id.Value, 
            value => new AssociateId(value));
        var squadConverter = new ValueConverter<SquadId, int>(
            id => id.Value, 
            value => new SquadId(value));

        builder.ToTable("squad_associates");
        builder.Ignore(c => c.Id);

        builder.HasKey(x => new { x.SquadId, x.AssociateId });
        builder.Property(x => x.SquadId)
            .HasColumnName("squad_id")
            .HasConversion(squadConverter);
        
        builder.Property(x => x.AssociateId)
            .HasColumnName("associate_id")
            .HasConversion(associateConverter);
        
        var configuration = new BaseSquadAssociateConfiguration<SquadAssociateAggregate>();
        configuration.Configure(builder);
        
        builder.HasOne(a => a.Associate)
            .WithMany(a => a.SquadAssociates)
            .HasForeignKey(a => a.AssociateId)
            .HasPrincipalKey(a => a.Id);
        
        builder.HasOne(a => a.Squad)
            .WithMany(a => a.SquadAssociates)
            .HasForeignKey(a => a.SquadId)
            .HasPrincipalKey(a => a.Id);
    }
}