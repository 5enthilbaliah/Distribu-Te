namespace DistribuTe.Aggregates.Teams.Infrastructure.Persistence.Specifications;

using DistribuTe.Domain.TeamEntities.Specifications;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class AssociateAggregateConfiguration : IEntityTypeConfiguration<AssociateAggregate>
{
    public void Configure(EntityTypeBuilder<AssociateAggregate> builder)
    {
        var converter = new ValueConverter<AssociateId, int>(
            id => id.Value, 
            value => new AssociateId(value));
        
        builder.ToTable("associates");
        builder.HasKey(a => a.Id);
            
        builder.Property(a => a.Id)
            .HasConversion(converter)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        var configuration = new BaseAssociateConfiguration<AssociateAggregate>();
        configuration.Configure(builder);
    }
}