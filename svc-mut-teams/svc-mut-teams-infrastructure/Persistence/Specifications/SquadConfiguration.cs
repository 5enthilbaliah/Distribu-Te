namespace DistribuTe.Mutators.Teams.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.AppEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class SquadConfiguration : IEntityTypeConfiguration<Squad>
{
    public void Configure(EntityTypeBuilder<Squad> builder)
    {
        var converter = new ValueConverter<SquadId, int>(
            id => id.Value, 
            value => new SquadId(value));
        
        builder.HasKey(a => a.Id);
            
        builder.Property(a => a.Id)
            .HasConversion(converter)
            .ValueGeneratedOnAdd();
        
        var configuration = new BaseSquadConfiguration<Squad>();
        configuration.Configure(builder);
    }
}