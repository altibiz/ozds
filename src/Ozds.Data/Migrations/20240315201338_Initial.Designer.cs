﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ozds.Data;

#nullable disable

namespace Ozds.Data.Migrations
{
    [DbContext(typeof(OzdsDbContext))]
    [Migration("20240315201338_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresExtension(modelBuilder, "timescaledb");
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LocationEntityRepresentativeEntity", b =>
                {
                    b.Property<string>("LocationsId")
                        .HasColumnType("text");

                    b.Property<string>("RepresentativesId")
                        .HasColumnType("text");

                    b.HasKey("LocationsId", "RepresentativesId");

                    b.HasIndex("RepresentativesId");

                    b.ToTable("LocationEntityRepresentativeEntity");
                });

            modelBuilder.Entity("NetworkUserEntityRepresentativeEntity", b =>
                {
                    b.Property<string>("NetworkUsersId")
                        .HasColumnType("text");

                    b.Property<string>("RepresentativesId")
                        .HasColumnType("text");

                    b.HasKey("NetworkUsersId", "RepresentativesId");

                    b.HasIndex("RepresentativesId");

                    b.ToTable("NetworkUserEntityRepresentativeEntity");
                });

            modelBuilder.Entity("Ozds.Data.Entities.AbbMeasurementEntity", b =>
                {
                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamptz");

                    b.Property<float>("ActiveEnergyExportTotal_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActiveEnergyImportTotal_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerExportL1_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerExportL2_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerExportL3_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerImportL1_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerImportL2_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerImportL3_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerL1_W")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerL2_W")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerL3_W")
                        .HasColumnType("real");

                    b.Property<float>("CurrentL1_A")
                        .HasColumnType("real");

                    b.Property<float>("CurrentL2_A")
                        .HasColumnType("real");

                    b.Property<float>("CurrentL3_A")
                        .HasColumnType("real");

                    b.Property<float>("ReactiveEnergyExportTotal_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactiveEnergyImportTotal_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerExportL1_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerExportL2_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerExportL3_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerImportL1_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerImportL2_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerImportL3_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerL1_VAR")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerL2_VAR")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerL3_VAR")
                        .HasColumnType("real");

                    b.Property<float>("VoltageL1_V")
                        .HasColumnType("real");

                    b.Property<float>("VoltageL2_V")
                        .HasColumnType("real");

                    b.Property<float>("VoltageL3_V")
                        .HasColumnType("real");

                    b.HasKey("Source", "Timestamp");

                    b.ToTable("AbbMeasurements");
                });

            modelBuilder.Entity("Ozds.Data.Entities.LocationEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Ozds.Data.Entities.NetworkUserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("NetworkUsers");
                });

            modelBuilder.Entity("Ozds.Data.Entities.RepresentativeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsOperatorRepresentative")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SocialSecurityNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Representatives");
                });

            modelBuilder.Entity("Ozds.Data.Entities.SchneiderMeasurementEntity", b =>
                {
                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamptz");

                    b.Property<float>("ActiveEnergyExportTotal_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActiveEnergyImportL1_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActiveEnergyImportL2_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActiveEnergyImportL3_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActiveEnergyImportTotal_Wh")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerL1_W")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerL2_W")
                        .HasColumnType("real");

                    b.Property<float>("ActivePowerL3_W")
                        .HasColumnType("real");

                    b.Property<float>("ApparentPowerTotal_VA")
                        .HasColumnType("real");

                    b.Property<float>("CurrentL1_A")
                        .HasColumnType("real");

                    b.Property<float>("CurrentL2_A")
                        .HasColumnType("real");

                    b.Property<float>("CurrentL3_A")
                        .HasColumnType("real");

                    b.Property<float>("ReactiveEnergyExportTotal_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactiveEnergyImportTotal_VARh")
                        .HasColumnType("real");

                    b.Property<float>("ReactivePowerTotal_VAR")
                        .HasColumnType("real");

                    b.Property<float>("VoltageL1_V")
                        .HasColumnType("real");

                    b.Property<float>("VoltageL2_V")
                        .HasColumnType("real");

                    b.Property<float>("VoltageL3_V")
                        .HasColumnType("real");

                    b.HasKey("Source", "Timestamp");

                    b.ToTable("SchneiderMeasurements");
                });

            modelBuilder.Entity("LocationEntityRepresentativeEntity", b =>
                {
                    b.HasOne("Ozds.Data.Entities.LocationEntity", null)
                        .WithMany()
                        .HasForeignKey("LocationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ozds.Data.Entities.RepresentativeEntity", null)
                        .WithMany()
                        .HasForeignKey("RepresentativesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NetworkUserEntityRepresentativeEntity", b =>
                {
                    b.HasOne("Ozds.Data.Entities.NetworkUserEntity", null)
                        .WithMany()
                        .HasForeignKey("NetworkUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ozds.Data.Entities.RepresentativeEntity", null)
                        .WithMany()
                        .HasForeignKey("RepresentativesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}