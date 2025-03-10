﻿namespace DistribuTe.Domain.DeploymentEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BaseEnvironmentConfiguration<TEnv> : IEntityTypeConfiguration<TEnv>
    where TEnv : BaseEnvironment
{
    public void Configure(EntityTypeBuilder<TEnv> builder)
    {
        builder.ToTable("environments");
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");
        
        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code")
            .HasColumnType("varchar(20)");
        
        builder.HasIndex(a => a.Code)
            .IsUnique();
        builder.HasIndex(a => a.Name)
            .IsUnique();

        builder.Property(e => e.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(e => e.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(e => e.CreatedBy)
            .IsRequired()
            .HasMaxLength(200)
            .HasColumnName("created_by")
            .HasColumnType("varchar(200)");
        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(200)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(200)");
    }
}