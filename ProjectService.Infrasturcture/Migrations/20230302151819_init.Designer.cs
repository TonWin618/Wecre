﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectService.Infrasturcture;

#nullable disable

namespace ProjectService.Infrasturcture.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20230302151819_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectService.Domain.Entities.FirmwareVerision", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("Downloads")
                        .HasColumnType("bigint");

                    b.Property<List<Guid>>("Files")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("FirmwareVerisions");
                });

            modelBuilder.Entity("ProjectService.Domain.Entities.ModelVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<long>("Downloads")
                        .HasColumnType("bigint");

                    b.Property<List<Guid>>("Files")
                        .IsRequired()
                        .HasColumnType("uuid[]");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ModelVersions");
                });

            modelBuilder.Entity("ProjectService.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ReadmeUrl")
                        .HasColumnType("uuid");

                    b.Property<string>("Tags")
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("UserName", "Name");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectService.Domain.Entities.ProjectVersion", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<Guid?>("FirmwareVersionsId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ModelVersionsId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<long>("TotalDownloads")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("FirmwareVersionsId");

                    b.HasIndex("ModelVersionsId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectVersions");
                });

            modelBuilder.Entity("ProjectService.Domain.Entities.FirmwareVerision", b =>
                {
                    b.HasOne("ProjectService.Domain.Entities.Project", "Project")
                        .WithMany("FirmwareVerisions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectService.Domain.Entities.ModelVersion", b =>
                {
                    b.HasOne("ProjectService.Domain.Entities.Project", "Project")
                        .WithMany("ModelVersions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectService.Domain.Entities.ProjectVersion", b =>
                {
                    b.HasOne("ProjectService.Domain.Entities.FirmwareVerision", "FirmwareVersions")
                        .WithMany()
                        .HasForeignKey("FirmwareVersionsId");

                    b.HasOne("ProjectService.Domain.Entities.ModelVersion", "ModelVersions")
                        .WithMany()
                        .HasForeignKey("ModelVersionsId");

                    b.HasOne("ProjectService.Domain.Entities.Project", "Project")
                        .WithMany("ProjectVersions")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FirmwareVersions");

                    b.Navigation("ModelVersions");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectService.Domain.Entities.Project", b =>
                {
                    b.Navigation("FirmwareVerisions");

                    b.Navigation("ModelVersions");

                    b.Navigation("ProjectVersions");
                });
#pragma warning restore 612, 618
        }
    }
}
