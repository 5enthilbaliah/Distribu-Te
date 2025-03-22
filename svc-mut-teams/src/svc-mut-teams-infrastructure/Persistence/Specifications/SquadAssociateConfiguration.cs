namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.TeamEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class SquadAssociateConfiguration : IEntityTypeConfiguration<SquadAssociate>
{
    public void Configure(EntityTypeBuilder<SquadAssociate> builder)
    {
        var associateConverter = new ValueConverter<AssociateId, int>(
            id => id.Value, 
            value => new AssociateId(value));
        var squadConverter = new ValueConverter<SquadId, int>(
            id => id.Value, 
            value => new SquadId(value));

        builder.Ignore(c => c.Id);

        builder.HasKey(x => new { x.SquadId, x.AssociateId });
        builder.Property(x => x.SquadId)
            .HasColumnName("squad_id")
            .HasConversion(squadConverter);
        
        builder.Property(x => x.AssociateId)
            .HasColumnName("associate_id")
            .HasConversion(associateConverter);
        
        var configuration = new BaseSquadAssociateConfiguration<SquadAssociate>();
        configuration.Configure(builder);
    }
}