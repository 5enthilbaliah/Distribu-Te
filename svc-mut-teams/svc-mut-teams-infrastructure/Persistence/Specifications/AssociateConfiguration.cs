namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.TeamEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class AssociateConfiguration : IEntityTypeConfiguration<Associate>
{
    public void Configure(EntityTypeBuilder<Associate> builder)
    {
        var converter = new ValueConverter<AssociateId, int>(
            id => id.Value, 
            value => new AssociateId(value));
        
        builder.HasKey(a => a.Id);
            
        builder.Property(a => a.Id)
            .HasConversion(converter)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        var configuration = new BaseAssociateConfiguration<Associate>();
        configuration.Configure(builder);
    }
}