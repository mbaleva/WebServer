﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PCShop.Data;

namespace PCShop.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PCShop.Data.Models.CPU", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Cache")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LogicalCores")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("NormalFrequency")
                        .HasColumnType("float");

                    b.Property<int>("PhysicalCores")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("TurboFrequency")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Processors");
                });

            modelBuilder.Entity("PCShop.Data.Models.GPU", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("DirectXSupport")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GraphicalProcessor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interfaces")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemoryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("GraphicalProcessor");
                });

            modelBuilder.Entity("PCShop.Data.Models.HardDrive", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Capacity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Interfaces")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("RPM")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HardDrives");
                });

            modelBuilder.Entity("PCShop.Data.Models.Laptop", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GraphicalProcessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MonitorInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("ProcessorId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RamMemoryId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("GraphicalProcessorId");

                    b.HasIndex("MemoryId");

                    b.HasIndex("ProcessorId");

                    b.HasIndex("RamMemoryId");

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("PCShop.Data.Models.RAM", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<int>("Frequency")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MemoryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Memory");
                });

            modelBuilder.Entity("WebServer.MVC.Identity.EntityFrameworkCore.Models.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PCShop.Data.Models.Laptop", b =>
                {
                    b.HasOne("PCShop.Data.Models.GPU", "GraphicalProcessor")
                        .WithMany()
                        .HasForeignKey("GraphicalProcessorId");

                    b.HasOne("PCShop.Data.Models.HardDrive", "Memory")
                        .WithMany()
                        .HasForeignKey("MemoryId");

                    b.HasOne("PCShop.Data.Models.CPU", "Processor")
                        .WithMany()
                        .HasForeignKey("ProcessorId");

                    b.HasOne("PCShop.Data.Models.RAM", "RamMemory")
                        .WithMany()
                        .HasForeignKey("RamMemoryId");

                    b.Navigation("GraphicalProcessor");

                    b.Navigation("Memory");

                    b.Navigation("Processor");

                    b.Navigation("RamMemory");
                });
#pragma warning restore 612, 618
        }
    }
}
