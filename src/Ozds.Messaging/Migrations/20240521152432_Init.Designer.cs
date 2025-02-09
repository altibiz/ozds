﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ozds.Messaging.Context;

#nullable disable

namespace Ozds.Messaging.Migrations
{
    [DbContext(typeof(MessagingDbContext))]
    [Migration("20240521152432_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ozds.Messaging.Sagas.NetworkUserInvoiceState", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .HasColumnType("uuid")
                        .HasColumnName("correlation_id");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("current_state");

                    b.Property<string>("NetworkUserInvoiceId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("network_user_invoice_id");

                    b.Property<string>("RegistrationId")
                        .HasColumnType("text")
                        .HasColumnName("registration_id");

                    b.Property<uint>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("CorrelationId")
                        .HasName("pk_network_user_invoice_state");

                    b.HasIndex("NetworkUserInvoiceId")
                        .IsUnique()
                        .HasDatabaseName("ix_network_user_invoice_state_network_user_invoice_id");

                    b.ToTable("network_user_invoice_state", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
