﻿// <auto-generated />
using System;
using DistribuTe.Infrastructure.AppDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    [DbContext(typeof(DistribuTeDbContext))]
    [Migration("20250224040213_DeploymentItemTasks")]
    partial class DeploymentItemTasks
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.AssociateAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("email_id");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("first_name");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(1)
                        .HasColumnType("char(1)")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("last_name");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(25)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("middle_name");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.HasKey("Id");

                    b.ToTable("associates", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActualEnd")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("actual_end");

                    b.Property<DateTime?>("ActualStart")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("actual_start");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("comments");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<int>("EnvironmentId")
                        .HasColumnType("int")
                        .HasColumnName("environment_id");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.Property<DateTime>("PlannedEnd")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("planned_end");

                    b.Property<DateTime>("PlannedStart")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("planned_start");

                    b.Property<int>("SquadId")
                        .HasColumnType("int")
                        .HasColumnName("squad_id");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("SquadId");

                    b.ToTable("deployments", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentItemAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActualEnd")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("actual_end");

                    b.Property<DateTime?>("ActualStart")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("actual_start");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("comments");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<int>("DeploymentId")
                        .HasColumnType("int")
                        .HasColumnName("deployment_id");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<int>("Sequence")
                        .HasColumnType("int")
                        .HasColumnName("sequence");

                    b.HasKey("Id");

                    b.HasIndex("DeploymentId");

                    b.HasIndex("ProjectId");

                    b.ToTable("deployment_items", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentItemTaskAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("ActualEnd")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("actual_end");

                    b.Property<DateTime?>("ActualStart")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("actual_start");

                    b.Property<int>("AssociateId")
                        .HasColumnType("int")
                        .HasColumnName("associate_id");

                    b.Property<string>("Comments")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("nvarchar(2000)")
                        .HasColumnName("comments");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<int>("DeploymentItemId")
                        .HasColumnType("int")
                        .HasColumnName("deployment_item_id");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<int>("Sequence")
                        .HasColumnType("int")
                        .HasColumnName("sequence");

                    b.HasKey("Id");

                    b.HasIndex("AssociateId");

                    b.HasIndex("DeploymentItemId");

                    b.ToTable("deployment_item_tasks", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentTaskTypeAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("deployment_task_types", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.EnvironmentAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("environments", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.ProjectAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("projects", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.ProjectCategoryAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("project_categories", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.SquadAggregate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("code");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("squads", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.SquadAssociateAggregate", b =>
                {
                    b.Property<int>("SquadId")
                        .HasColumnType("int")
                        .HasColumnName("squad_id");

                    b.Property<int>("AssociateId")
                        .HasColumnType("int")
                        .HasColumnName("associate_id");

                    b.Property<decimal>("Capacity")
                        .HasColumnType("decimal(5,4)")
                        .HasColumnName("capacity");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<DateTime?>("EndedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("ended_on");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("started_on");

                    b.HasKey("SquadId", "AssociateId");

                    b.HasIndex("AssociateId");

                    b.ToTable("squad_associates", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.SquadProjectAggregate", b =>
                {
                    b.Property<int>("SquadId")
                        .HasColumnType("int")
                        .HasColumnName("squad_id");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int")
                        .HasColumnName("project_id");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("created_by");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("created_on");

                    b.Property<DateTime?>("EndedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("ended_on");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(45)
                        .HasColumnType("varchar(45)")
                        .HasColumnName("modified_by");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("modified_on");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2(7)")
                        .HasColumnName("started_on");

                    b.HasKey("SquadId", "ProjectId");

                    b.HasIndex("ProjectId");

                    b.ToTable("squad_projects", (string)null);
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentAggregate", b =>
                {
                    b.HasOne("DistribuTe.Domain.AppEntities.EnvironmentAggregate", "Environment")
                        .WithMany("Deployments")
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistribuTe.Domain.AppEntities.SquadAggregate", "Squad")
                        .WithMany("Deployments")
                        .HasForeignKey("SquadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Environment");

                    b.Navigation("Squad");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentItemAggregate", b =>
                {
                    b.HasOne("DistribuTe.Domain.AppEntities.DeploymentAggregate", "Deployment")
                        .WithMany("DeploymentItems")
                        .HasForeignKey("DeploymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistribuTe.Domain.AppEntities.ProjectAggregate", "Project")
                        .WithMany("DeploymentItems")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deployment");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentItemTaskAggregate", b =>
                {
                    b.HasOne("DistribuTe.Domain.AppEntities.AssociateAggregate", "Associate")
                        .WithMany("DeploymentItemTasks")
                        .HasForeignKey("AssociateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistribuTe.Domain.AppEntities.DeploymentItemAggregate", "DeploymentItem")
                        .WithMany("DeploymentItemTasks")
                        .HasForeignKey("DeploymentItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Associate");

                    b.Navigation("DeploymentItem");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.ProjectAggregate", b =>
                {
                    b.HasOne("DistribuTe.Domain.AppEntities.ProjectCategoryAggregate", "Category")
                        .WithMany("Projects")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.SquadAssociateAggregate", b =>
                {
                    b.HasOne("DistribuTe.Domain.AppEntities.AssociateAggregate", "Associate")
                        .WithMany("SquadAssociates")
                        .HasForeignKey("AssociateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistribuTe.Domain.AppEntities.SquadAggregate", "Squad")
                        .WithMany("SquadAssociates")
                        .HasForeignKey("SquadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Associate");

                    b.Navigation("Squad");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.SquadProjectAggregate", b =>
                {
                    b.HasOne("DistribuTe.Domain.AppEntities.ProjectAggregate", "Project")
                        .WithMany("SquadProjects")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DistribuTe.Domain.AppEntities.SquadAggregate", "Squad")
                        .WithMany("SquadProjects")
                        .HasForeignKey("SquadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("Squad");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.AssociateAggregate", b =>
                {
                    b.Navigation("DeploymentItemTasks");

                    b.Navigation("SquadAssociates");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentAggregate", b =>
                {
                    b.Navigation("DeploymentItems");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.DeploymentItemAggregate", b =>
                {
                    b.Navigation("DeploymentItemTasks");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.EnvironmentAggregate", b =>
                {
                    b.Navigation("Deployments");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.ProjectAggregate", b =>
                {
                    b.Navigation("DeploymentItems");

                    b.Navigation("SquadProjects");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.ProjectCategoryAggregate", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.SquadAggregate", b =>
                {
                    b.Navigation("Deployments");

                    b.Navigation("SquadAssociates");

                    b.Navigation("SquadProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
