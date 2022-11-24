﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectsManager.DAL.Context;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    [DbContext(typeof(ProjectsManagerDb))]
    [Migration("20221123071525_ForeignKeysUpdate")]
    partial class ForeignKeysUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.Property<int>("InvolvedEmployeesId")
                        .HasColumnType("int");

                    b.Property<int>("ParticipatedProjectsId")
                        .HasColumnType("int");

                    b.HasKey("InvolvedEmployeesId", "ParticipatedProjectsId");

                    b.HasIndex("ParticipatedProjectsId");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("ProjectsManager.DAL.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ProjectsManager.DAL.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ProjectsManager.DAL.Entities.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AuthorEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExecutorEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Priority")
                        .HasColumnType("bigint");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AuthorEmployeeId");

                    b.HasIndex("ExecutorEmployeeId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("ProjectsManager.DAL.Entities.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ContractorCompanyId")
                        .HasColumnType("int");

                    b.Property<int?>("CustomerCompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Priority")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("date");

                    b.Property<int?>("TeamLeaderEmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContractorCompanyId");

                    b.HasIndex("CustomerCompanyId");

                    b.HasIndex("TeamLeaderEmployeeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("EmployeeProject", b =>
                {
                    b.HasOne("ProjectsManager.DAL.Entities.Employee", null)
                        .WithMany()
                        .HasForeignKey("InvolvedEmployeesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectsManager.DAL.Entities.Project", null)
                        .WithMany()
                        .HasForeignKey("ParticipatedProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectsManager.DAL.Entities.Goal", b =>
                {
                    b.HasOne("ProjectsManager.DAL.Entities.Employee", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorEmployeeId");

                    b.HasOne("ProjectsManager.DAL.Entities.Employee", "Executor")
                        .WithMany("Goals")
                        .HasForeignKey("ExecutorEmployeeId");

                    b.Navigation("Author");

                    b.Navigation("Executor");
                });

            modelBuilder.Entity("ProjectsManager.DAL.Entities.Project", b =>
                {
                    b.HasOne("ProjectsManager.DAL.Entities.Company", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorCompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectsManager.DAL.Entities.Company", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerCompanyId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ProjectsManager.DAL.Entities.Employee", "TeamLeader")
                        .WithMany()
                        .HasForeignKey("TeamLeaderEmployeeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Contractor");

                    b.Navigation("Customer");

                    b.Navigation("TeamLeader");
                });

            modelBuilder.Entity("ProjectsManager.DAL.Entities.Employee", b =>
                {
                    b.Navigation("Goals");
                });
#pragma warning restore 612, 618
        }
    }
}
