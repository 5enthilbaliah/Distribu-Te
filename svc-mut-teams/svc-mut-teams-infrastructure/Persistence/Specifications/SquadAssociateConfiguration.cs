namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.AppEntities.Specifications;
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
        
        builder.HasKey(s => s.Id);
        var complexPropBuilder = builder.ComplexProperty(s => s.Id);
        complexPropBuilder
            .Property(s => s.AssociateId)
            .HasColumnName("associate_id")
            .HasConversion(associateConverter);
        complexPropBuilder
            .Property(s => s.SquadId)
            .HasColumnName("squad_id")
            .HasConversion(squadConverter);
        
        var configuration = new BaseSquadAssociateConfiguration<SquadAssociate>();
        configuration.Configure(builder);
    }
}