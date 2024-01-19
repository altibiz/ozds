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
    [Migration("20240124160305_Initial")]
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

            modelBuilder.Entity("Ozds.Data.Entities.AbbMeasurementEntity", b =>
                {
                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamptz");

                    b.Property<double>("ActiveEnergyExportTotal_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActiveEnergyImportTotal_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerExportL1_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerExportL2_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerExportL3_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerImportL1_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerImportL2_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerImportL3_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerL1_W")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerL2_W")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerL3_W")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentL1_A")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentL2_A")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentL3_A")
                        .HasColumnType("double precision");

                    b.Property<long>("Milliseconds")
                        .HasColumnType("bigint");

                    b.Property<double>("ReactiveEnergyExportTotal_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactiveEnergyImportTotal_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerExportL1_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerExportL2_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerExportL3_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerImportL1_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerImportL2_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerImportL3_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerL1_VAR")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerL2_VAR")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerL3_VAR")
                        .HasColumnType("double precision");

                    b.Property<double>("VoltageL1_V")
                        .HasColumnType("double precision");

                    b.Property<double>("VoltageL2_V")
                        .HasColumnType("double precision");

                    b.Property<double>("VoltageL3_V")
                        .HasColumnType("double precision");

                    b.HasKey("Source", "Timestamp");

                    b.HasIndex("Source", "Milliseconds");

                    b.HasIndex("Source", "Timestamp");

                    b.ToTable("AbbMeasurements");
                });

            modelBuilder.Entity("Ozds.Data.Entities.SchneiderMeasurementEntity", b =>
                {
                    b.Property<string>("Source")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamptz");

                    b.Property<double>("ActiveEnergyExportTotal_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActiveEnergyImportL1_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActiveEnergyImportL2_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActiveEnergyImportL3_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActiveEnergyImportTotal_Wh")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerL1_W")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerL2_W")
                        .HasColumnType("double precision");

                    b.Property<double>("ActivePowerL3_W")
                        .HasColumnType("double precision");

                    b.Property<double>("ApparentPowerTotal_VA")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentL1_A")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentL2_A")
                        .HasColumnType("double precision");

                    b.Property<double>("CurrentL3_A")
                        .HasColumnType("double precision");

                    b.Property<long>("Milliseconds")
                        .HasColumnType("bigint");

                    b.Property<double>("ReactiveEnergyExportTotal_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactiveEnergyImportTotal_VARh")
                        .HasColumnType("double precision");

                    b.Property<double>("ReactivePowerTotal_VAR")
                        .HasColumnType("double precision");

                    b.Property<double>("VoltageL1_V")
                        .HasColumnType("double precision");

                    b.Property<double>("VoltageL2_V")
                        .HasColumnType("double precision");

                    b.Property<double>("VoltageL3_V")
                        .HasColumnType("double precision");

                    b.HasKey("Source", "Timestamp");

                    b.HasIndex("Source", "Milliseconds");

                    b.HasIndex("Source", "Timestamp");

                    b.ToTable("SchneiderMeasurements");
                });
#pragma warning restore 612, 618
        }
    }
}
