﻿// <auto-generated />
using System;
using DogHouseService.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DogHouseService.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DogHouseService.Core.Domain.Models.Dog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TailLength")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Dogs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ba11df00-f686-418c-906e-db8c530fe81b"),
                            Color = "red&amber",
                            Name = "Neo",
                            TailLength = 22,
                            Weight = 32
                        },
                        new
                        {
                            Id = new Guid("5e4c9b73-aad8-42b6-b166-50c1cece9815"),
                            Color = "black&white",
                            Name = "Jessy",
                            TailLength = 7,
                            Weight = 14
                        },
                        new
                        {
                            Id = new Guid("73c99b57-fb06-4bd6-a7d8-8d1d2939ad80"),
                            Color = "White",
                            Name = "Spot",
                            TailLength = 2,
                            Weight = 20
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
