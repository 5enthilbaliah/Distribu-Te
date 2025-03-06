namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamEntities.Specifications;

public class AssociateAggregateConfiguration : IEntityTypeConfiguration<AssociateAggregate>
{
    public void Configure(EntityTypeBuilder<AssociateAggregate> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");
        
        var configuration = new BaseAssociateConfiguration<AssociateAggregate>();
        configuration.Configure(builder);
    }
}