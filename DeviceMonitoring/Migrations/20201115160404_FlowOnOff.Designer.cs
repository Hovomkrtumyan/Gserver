﻿// <auto-generated />
using System;
using DeviceMonitoring.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeviceMonitoring.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20201115160404_FlowOnOff")]
    partial class FlowOnOff
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DeviceMonitoring.Entities.DeviceData", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Dpdrac")
                        .HasColumnType("float");

                    b.Property<double>("Dpgorcakic")
                        .HasColumnType("float");

                    b.Property<double>("Dppastaci")
                        .HasColumnType("float");

                    b.Property<double>("Flowhanac")
                        .HasColumnType("float");

                    b.Property<double>("Flowmax")
                        .HasColumnType("float");

                    b.Property<double>("Flowpast")
                        .HasColumnType("float");

                    b.Property<double>("Flowproc")
                        .HasColumnType("float");

                    b.Property<double>("Flowsarqac")
                        .HasColumnType("float");

                    b.Property<double>("Kgorcakic")
                        .HasColumnType("float");

                    b.Property<double>("Onoff")
                        .HasColumnType("float");

                    b.Property<double>("Pressgorcakic")
                        .HasColumnType("float");

                    b.Property<double>("Presspastaci")
                        .HasColumnType("float");

                    b.Property<double>("Selfonoff")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedDt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DeviceData");
                });

            modelBuilder.Entity("DeviceMonitoring.Entities.DeviceSettings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeviceId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Dpgorcakic")
                        .HasColumnType("float");

                    b.Property<double>("Flowhanac")
                        .HasColumnType("float");

                    b.Property<double>("Flowmax")
                        .HasColumnType("float");

                    b.Property<double>("Flowproc")
                        .HasColumnType("float");

                    b.Property<double>("Kgorcakic")
                        .HasColumnType("float");

                    b.Property<bool>("Onoff")
                        .HasColumnType("bit");

                    b.Property<double>("Pressgorcakic")
                        .HasColumnType("float");

                    b.Property<DateTime>("UpdatedDt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("DeviceSettings");
                });

            modelBuilder.Entity("DeviceMonitoring.Entities.FlowSettings", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDt")
                        .HasColumnType("datetime2");

                    b.Property<double>("FlowAuto")
                        .HasColumnType("float");

                    b.Property<bool>("On")
                        .HasColumnType("bit");

                    b.Property<DateTime>("UpdatedDt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("FlowSettings");
                });
#pragma warning restore 612, 618
        }
    }
}
