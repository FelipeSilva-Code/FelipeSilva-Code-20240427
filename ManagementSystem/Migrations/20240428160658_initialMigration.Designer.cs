﻿// <auto-generated />
using ManagementSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ManagementSystem.Migrations
{
    [DbContext(typeof(ManagementSystemDbContext))]
    [Migration("20240428160658_initialMigration")]
    partial class initialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ManagementSystem.Models.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("PkId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DsName");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("TgInactive");

                    b.Property<int>("UnityId")
                        .HasColumnType("integer")
                        .HasColumnName("FkUnity");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("FkUser");

                    b.HasKey("Id");

                    b.HasIndex("UnityId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("ManagementSystem.Models.UnityEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("PkId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DsCode");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DsName");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("TgInactive");

                    b.HasKey("Id");

                    b.ToTable("Unity");
                });

            modelBuilder.Entity("ManagementSystem.Models.UserEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("PkId");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DsLogin");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("DsPassword");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("TgInactive");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ManagementSystem.Models.EmployeeEntity", b =>
                {
                    b.HasOne("ManagementSystem.Models.UnityEntity", "Unity")
                        .WithMany("Employees")
                        .HasForeignKey("UnityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ManagementSystem.Models.UserEntity", "User")
                        .WithOne("Employee")
                        .HasForeignKey("ManagementSystem.Models.EmployeeEntity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ManagementSystem.Models.UnityEntity", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("ManagementSystem.Models.UserEntity", b =>
                {
                    b.Navigation("Employee")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
