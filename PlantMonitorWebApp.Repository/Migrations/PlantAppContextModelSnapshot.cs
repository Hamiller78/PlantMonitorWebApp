﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlantMonitorWebApp.Repository;

#nullable disable

namespace PlantMonitorWebApp.Repository.Migrations
{
    [DbContext(typeof(PlantAppContext))]
    partial class PlantAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("PlantMonitorWebApp.Shared.Models.Plant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SensorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.ToTable("Plant", (string)null);
                });

            modelBuilder.Entity("PlantMonitorWebApp.Shared.Models.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ServiceUri")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Sensor", (string)null);
                });

            modelBuilder.Entity("PlantMonitorWebApp.Shared.Models.Plant", b =>
                {
                    b.HasOne("PlantMonitorWebApp.Shared.Models.Sensor", "Sensor")
                        .WithMany()
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sensor");
                });
#pragma warning restore 612, 618
        }
    }
}
