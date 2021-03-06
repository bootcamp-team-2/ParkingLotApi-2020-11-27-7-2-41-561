﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParkingLotApi.Repository;

namespace ParkingLotApi.Migrations
{
    [DbContext(typeof(ParkingLotContext))]
    [Migration("20201129125236_MakeCloseTimeNullable")]
    partial class MakeCloseTimeNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ParkingLotApi.Entities.OrderEntity", b =>
                {
                    b.Property<string>("OrderNumber")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset?>("CloseTimeOffset")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset>("CreationTimeOffset")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ParkingLotName")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PlateNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("OrderNumber");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ParkingLotApi.Entities.ParkingLotEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("ParkingLots");
                });
#pragma warning restore 612, 618
        }
    }
}
