﻿namespace DistribuTe.Domain.AppEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ProjectCategoryConfiguration<TProjCat> : IEntityTypeConfiguration<TProjCat>
    where TProjCat : ProjectCategory
{
    public void Configure(EntityTypeBuilder<TProjCat> builder)
    {
        builder.ToTable("project_categories");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("id");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");
        
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20)
            .HasColumnName("code")
            .HasColumnType("varchar(20)");
        
        builder.Property(p => p.Description)
            .HasMaxLength(200)
            .HasColumnName("description")
            .HasColumnType("varchar(200)");

        builder.Property(p => p.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(p => p.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(p => p.CreatedBy)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("created_by")
            .HasColumnType("varchar(45)");
        builder.Property(p => p.ModifiedBy)
            .HasMaxLength(45)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(45)");
    }
}

public class ProjectCategoryAggregateConfiguration : IEntityTypeConfiguration<ProjectCategoryAggregate>
{
    public void Configure(EntityTypeBuilder<ProjectCategoryAggregate> builder)
    {
        var configuration = new ProjectCategoryConfiguration<ProjectCategoryAggregate>();
        configuration.Configure(builder);
    }
}