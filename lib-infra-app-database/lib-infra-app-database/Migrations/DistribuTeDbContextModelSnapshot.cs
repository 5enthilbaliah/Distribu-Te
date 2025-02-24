﻿// <auto-generated />
using System;
using DistribuTe.Infrastructure.AppDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DistribuTe.Infrastructure.AppDatabase.Migrations
{
    [DbContext(typeof(DistribuTeDbContext))]
    partial class DistribuTeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                    b.Navigation("SquadAssociates");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.ProjectAggregate", b =>
                {
                    b.Navigation("SquadProjects");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.ProjectCategoryAggregate", b =>
                {
                    b.Navigation("Projects");
                });

            modelBuilder.Entity("DistribuTe.Domain.AppEntities.SquadAggregate", b =>
                {
                    b.Navigation("SquadAssociates");

                    b.Navigation("SquadProjects");
                });
#pragma warning restore 612, 618
        }
    }
}
