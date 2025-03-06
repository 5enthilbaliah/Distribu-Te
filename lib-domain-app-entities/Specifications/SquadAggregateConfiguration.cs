namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamEntities.Specifications;

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