namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.TeamEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class SquadAggregateConfiguration : IEntityTypeConfiguration<SquadAggregate>
{
    public void Configure(EntityTypeBuilder<SquadAggregate> builder)
    {
        var converter = new ValueConverter<SquadId, int>(
            id => id.Value, 
            value => new SquadId(value));
        
        builder.ToTable("squads");
        builder.HasKey(a => a.Id);
            
        builder.Property(a => a.Id)
            .HasConversion(converter)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        var configuration = new BaseSquadConfiguration<SquadAggregate>();
        configuration.Configure(builder);
    }
}