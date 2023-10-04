﻿// <auto-generated />
using System;
using FestiFindV5.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FestiFindV5.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230928120136_build")]
    partial class build
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FestFindV2.Models.Cashier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Cashiers");
                });

            modelBuilder.Entity("FestFindV2.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Costs")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date_Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxParticipants")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrganizerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizerId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("FestFindV2.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("EventId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("ParticipantId")
                        .HasColumnType("int");

                    b.Property<bool>("Payed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("FestFindV2.Models.Organizer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Organizers");
                });

            modelBuilder.Entity("FestFindV2.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EMail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Participants");
                });

            modelBuilder.Entity("FestFindV2.Models.Cashier", b =>
                {
                    b.HasOne("FestFindV2.Models.Event", null)
                        .WithMany("Cashiers")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("FestFindV2.Models.Event", b =>
                {
                    b.HasOne("FestFindV2.Models.Organizer", null)
                        .WithMany("events")
                        .HasForeignKey("OrganizerId");
                });

            modelBuilder.Entity("FestFindV2.Models.Order", b =>
                {
                    b.HasOne("FestFindV2.Models.Event", "Event")
                        .WithMany("Orders")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FestFindV2.Models.Participant", null)
                        .WithMany("Orders")
                        .HasForeignKey("ParticipantId");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("FestFindV2.Models.Participant", b =>
                {
                    b.HasOne("FestFindV2.Models.Event", null)
                        .WithMany("Participants")
                        .HasForeignKey("EventId");
                });

            modelBuilder.Entity("FestFindV2.Models.Event", b =>
                {
                    b.Navigation("Cashiers");

                    b.Navigation("Orders");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("FestFindV2.Models.Organizer", b =>
                {
                    b.Navigation("events");
                });

            modelBuilder.Entity("FestFindV2.Models.Participant", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
