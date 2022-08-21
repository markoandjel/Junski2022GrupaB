﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Template.Migrations
{
    [DbContext(typeof(IspitDbContext))]
    [Migration("20220820220936_v2")]
    partial class v2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Automobil", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BojaId")
                        .HasColumnType("int");

                    b.Property<int?>("MarkaId")
                        .HasColumnType("int");

                    b.Property<int?>("ModelId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BojaId");

                    b.HasIndex("MarkaId");

                    b.HasIndex("ModelId");

                    b.ToTable("Automobili");
                });

            modelBuilder.Entity("Models.Boja", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ModelId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ModelId");

                    b.ToTable("Boje");
                });

            modelBuilder.Entity("Models.Marka", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Marke");
                });

            modelBuilder.Entity("Models.Model", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MarkaId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("MarkaId");

                    b.ToTable("Modeli");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prodavnice");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AutomobilSpojId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int>("Kolicina")
                        .HasColumnType("int");

                    b.Property<int?>("ProdavnicaSpojId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutomobilSpojId");

                    b.HasIndex("ProdavnicaSpojId");

                    b.ToTable("Spojevi");
                });

            modelBuilder.Entity("Models.Automobil", b =>
                {
                    b.HasOne("Models.Boja", "Boja")
                        .WithMany()
                        .HasForeignKey("BojaId");

                    b.HasOne("Models.Marka", "Marka")
                        .WithMany()
                        .HasForeignKey("MarkaId");

                    b.HasOne("Models.Model", "Model")
                        .WithMany()
                        .HasForeignKey("ModelId");

                    b.Navigation("Boja");

                    b.Navigation("Marka");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Models.Boja", b =>
                {
                    b.HasOne("Models.Model", "Model")
                        .WithMany("Boje")
                        .HasForeignKey("ModelId");

                    b.Navigation("Model");
                });

            modelBuilder.Entity("Models.Model", b =>
                {
                    b.HasOne("Models.Marka", "Marka")
                        .WithMany("Modeli")
                        .HasForeignKey("MarkaId");

                    b.Navigation("Marka");
                });

            modelBuilder.Entity("Models.Spoj", b =>
                {
                    b.HasOne("Models.Automobil", "AutomobilSpoj")
                        .WithMany("Spojevi")
                        .HasForeignKey("AutomobilSpojId");

                    b.HasOne("Models.Prodavnica", "ProdavnicaSpoj")
                        .WithMany("Spojevi")
                        .HasForeignKey("ProdavnicaSpojId");

                    b.Navigation("AutomobilSpoj");

                    b.Navigation("ProdavnicaSpoj");
                });

            modelBuilder.Entity("Models.Automobil", b =>
                {
                    b.Navigation("Spojevi");
                });

            modelBuilder.Entity("Models.Marka", b =>
                {
                    b.Navigation("Modeli");
                });

            modelBuilder.Entity("Models.Model", b =>
                {
                    b.Navigation("Boje");
                });

            modelBuilder.Entity("Models.Prodavnica", b =>
                {
                    b.Navigation("Spojevi");
                });
#pragma warning restore 612, 618
        }
    }
}