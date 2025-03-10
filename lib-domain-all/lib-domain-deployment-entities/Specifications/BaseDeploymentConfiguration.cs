﻿namespace DistribuTe.Domain.DeploymentEntities.Specifications;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BaseDeploymentConfiguration<TDeploy> : IEntityTypeConfiguration<TDeploy>
    where TDeploy : BaseDeployment
{
    public void Configure(EntityTypeBuilder<TDeploy> builder)
    {
        builder.ToTable("deployments");
        
        builder.Property(d => d.Name)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("name")
            .HasColumnType("varchar(45)");
        
        builder.HasIndex(a => a.Name)
            .IsUnique();

        builder.Property(d => d.SquadId)
            .HasColumnName("squad_id");
        builder.Property(d => d.EnvironmentId)
            .HasColumnName("environment_id");
        
        builder.Property(d => d.PlannedStart)
            .HasColumnType("datetime2(7)")
            .HasColumnName("planned_start");
        builder.Property(d => d.PlannedEnd)
            .HasColumnType("datetime2(7)")
            .HasColumnName("planned_end");
        
        builder.Property(d => d.ActualStart)
            .HasColumnType("datetime2(7)")
            .HasColumnName("actual_start");
        builder.Property(d => d.ActualEnd)
            .HasColumnType("datetime2(7)")
            .HasColumnName("actual_end");
        
        builder.Property(d => d.Comments)
            .IsRequired()
            .HasMaxLength(2000)
            .HasColumnName("comments");
        
        builder.Property(d => d.CreatedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("created_on");
        builder.Property(d => d.ModifiedOn)
            .HasColumnType("datetime2(7)")
            .HasColumnName("modified_on");

        builder.Property(d => d.CreatedBy)
            .IsRequired()
            .HasMaxLength(45)
            .HasColumnName("created_by")
            .HasColumnType("varchar(45)");
        builder.Property(d => d.ModifiedBy)
            .HasMaxLength(45)
            .HasColumnName("modified_by")
            .HasColumnType("varchar(45)");
        
        builder.Property(d => d.StatusId)
            .HasColumnName("status_id");
    }
}