namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AssociateConfiguration<TAssociate> : IEntityTypeConfiguration<TAssociate>
    where TAssociate : Associate
{
    public void Configure(EntityTypeBuilder<TAssociate> builder)
    {
        builder.ToTable("associates");

        builder.HasKey(a => a.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(a => a.FirstName)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("first_name")
            .HasColumnType("varchar(45)");
        
        builder.Property(a => a.LastName)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("last_name")
            .HasColumnType("varchar(45)");
        
        builder.Property(a => a.MiddleName)
            .HasMaxLength(25)
            .HasColumnName("middle_name")
            .HasColumnType("varchar(45)");
        
        builder.Property(a => a.EmailId)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("email_id")
            .HasColumnType("varchar(45)");

        builder.Property(a => a.Gender)
            .IsRequired()
            .HasMaxLength(1)
            .HasColumnType("char(1)")
            .HasColumnName("gender");

        builder.Property(a => a.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(a => a.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(a => a.CreatedBy)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("created_by")
            .HasColumnType("varchar(45)");
        builder.Property(a => a.ModifiedBy)
            .HasMaxLength(45)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(45)");
    }
}

public class AssociateAggregateConfiguration : IEntityTypeConfiguration<AssociateAggregate>
{
    public void Configure(EntityTypeBuilder<AssociateAggregate> builder)
    {
        var configuration = new AssociateConfiguration<AssociateAggregate>();
        configuration.Configure(builder);
        
        builder.HasMany(a => a.SquadAssociates)
            .WithOne(a => a.Associate)
            .HasForeignKey(a => a.AssociateId)
            .HasPrincipalKey(a => a.Id);
        
        builder.HasMany(a => a.DeploymentItemTasks)
            .WithOne(a => a.Associate)
            .HasForeignKey(a => a.AssociateId)
            .HasPrincipalKey(a => a.Id);
    }
}