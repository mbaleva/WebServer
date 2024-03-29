﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Template.Data;

namespace Template.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211013132958_Add new Entities")]
    partial class AddnewEntities
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Template.Data.Models.CPU", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cache")
                        .HasColumnType("int");

                    b.Property<int>("CountCores")
                        .HasColumnType("int");

                    b.Property<double>("Hz")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CPU");
                });

            modelBuilder.Entity("Template.Data.Models.GPU", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("MemoryCapacity")
                        .HasColumnType("int");

                    b.Property<string>("MemoryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("GPU");
                });

            modelBuilder.Entity("Template.Data.Models.HDD", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Cache")
                        .HasColumnType("int");

                    b.Property<string>("Capacity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("HDD");
                });

            modelBuilder.Entity("Template.Data.Models.Laptops", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CPUId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CPU_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GPUId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("GPU_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HDDId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HDD_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("RAMId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RAM_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("CPUId");

                    b.HasIndex("GPUId");

                    b.HasIndex("HDDId");

                    b.HasIndex("RAMId");

                    b.HasIndex("UserId");

                    b.ToTable("Laptops");
                });

            modelBuilder.Entity("Template.Data.Models.RAM", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Hz")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MemoryCapacity")
                        .HasColumnType("int");

                    b.Property<string>("MemoryType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RAM");
                });

            modelBuilder.Entity("WebServer.MVC.Identity.EntityFrameworkCore.Models.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

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

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Template.Data.Models.Users", b =>
                {
                    b.HasBaseType("WebServer.MVC.Identity.EntityFrameworkCore.Models.IdentityUser");

                    b.HasDiscriminator().HasValue("Users");
                });

            modelBuilder.Entity("Template.Data.Models.CPU", b =>
                {
                    b.HasOne("Template.Data.Models.Users", "User")
                        .WithMany("CPUs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Template.Data.Models.GPU", b =>
                {
                    b.HasOne("Template.Data.Models.Users", "User")
                        .WithMany("GPUs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Template.Data.Models.HDD", b =>
                {
                    b.HasOne("Template.Data.Models.Users", "User")
                        .WithMany("HDDs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Template.Data.Models.Laptops", b =>
                {
                    b.HasOne("Template.Data.Models.CPU", "CPU")
                        .WithMany("Laptops")
                        .HasForeignKey("CPUId");

                    b.HasOne("Template.Data.Models.GPU", "GPU")
                        .WithMany("Laptops")
                        .HasForeignKey("GPUId");

                    b.HasOne("Template.Data.Models.HDD", "HDD")
                        .WithMany("Laptops")
                        .HasForeignKey("HDDId");

                    b.HasOne("Template.Data.Models.RAM", "RAM")
                        .WithMany("Laptops")
                        .HasForeignKey("RAMId");

                    b.HasOne("Template.Data.Models.Users", "User")
                        .WithMany("Laptops")
                        .HasForeignKey("UserId");

                    b.Navigation("CPU");

                    b.Navigation("GPU");

                    b.Navigation("HDD");

                    b.Navigation("RAM");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Template.Data.Models.RAM", b =>
                {
                    b.HasOne("Template.Data.Models.Users", "User")
                        .WithMany("RAMs")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Template.Data.Models.CPU", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("Template.Data.Models.GPU", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("Template.Data.Models.HDD", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("Template.Data.Models.RAM", b =>
                {
                    b.Navigation("Laptops");
                });

            modelBuilder.Entity("Template.Data.Models.Users", b =>
                {
                    b.Navigation("CPUs");

                    b.Navigation("GPUs");

                    b.Navigation("HDDs");

                    b.Navigation("Laptops");

                    b.Navigation("RAMs");
                });
#pragma warning restore 612, 618
        }
    }
}
