namespace DistribuTe.Domain.TeamEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BaseAssociateConfiguration<TAssociate> : IEntityTypeConfiguration<TAssociate>
    where TAssociate : BaseAssociate
{
    public void Configure(EntityTypeBuilder<TAssociate> builder)
    {
        builder.ToTable("associates");

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

        builder.HasIndex(a => a.EmailId)
            .IsUnique();

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