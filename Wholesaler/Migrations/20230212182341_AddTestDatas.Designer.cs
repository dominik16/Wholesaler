﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Wholesaler.Data;

#nullable disable

namespace Wholesaler.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230212182341_AddTestDatas")]
    partial class AddTestDatas
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Wholesaler.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<int>("StorageId")
                        .HasColumnType("integer");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StorageId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Kabel prądowy",
                            Name = "Kabel YDY",
                            Price = 99.989999999999995,
                            StorageId = 1,
                            Unit = "m/b"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Żarówka do świecenia",
                            Name = "Żarówka 100W",
                            Price = 10.99,
                            StorageId = 1,
                            Unit = "szt"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Śrubokręt krzyżakowy",
                            Name = "Śrubokręt",
                            Price = 5.0,
                            StorageId = 2,
                            Unit = "szt"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Kabel wysokonapięciowy",
                            Name = "Kabel RC",
                            Price = 800.0,
                            StorageId = 2,
                            Unit = "m/b"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Narzędzie - nóż",
                            Name = "Nóż do tapet",
                            Price = 1.99,
                            StorageId = 1,
                            Unit = "szt"
                        });
                });

            modelBuilder.Entity("Wholesaler.Models.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Storages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Graniczna 12",
                            City = "Kraków",
                            Name = "Magazyn Wewnętrzny",
                            Type = "Detaliczny"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Wesoła 46",
                            City = "Rzeszów",
                            Name = "Magazyn Zewnętrzny Zadaszony",
                            Type = "Hurtowy"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Słoneczna 2",
                            City = "Gdańsk",
                            Name = "Magazyn Niezadaszony",
                            Type = "Hurtowy"
                        });
                });

            modelBuilder.Entity("Wholesaler.Models.Product", b =>
                {
                    b.HasOne("Wholesaler.Models.Storage", "Storage")
                        .WithMany("Products")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Wholesaler.Models.Storage", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
