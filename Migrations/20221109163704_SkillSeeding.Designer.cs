﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dotnet_rpg.Data;

#nullable disable

namespace dotnet_rpg.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221109163704_SkillSeeding")]
    partial class SkillSeeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CharecterSkill", b =>
                {
                    b.Property<int>("CharectersId")
                        .HasColumnType("int");

                    b.Property<int>("SkillsId")
                        .HasColumnType("int");

                    b.HasKey("CharectersId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("CharecterSkill");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Charecter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("HitPoints")
                        .HasColumnType("int");

                    b.Property<int>("Intelligence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Charecters");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Damage = 30,
                            Name = "Fireball"
                        },
                        new
                        {
                            Id = 2,
                            Damage = 20,
                            Name = "Frenzy"
                        },
                        new
                        {
                            Id = 3,
                            Damage = 50,
                            Name = "Blizzard"
                        });
                });

            modelBuilder.Entity("dotnet_rpg.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CharecterId")
                        .HasColumnType("int");

                    b.Property<int>("Damage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharecterId")
                        .IsUnique();

                    b.ToTable("Weapons");
                });

            modelBuilder.Entity("CharecterSkill", b =>
                {
                    b.HasOne("dotnet_rpg.Models.Charecter", null)
                        .WithMany()
                        .HasForeignKey("CharectersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("dotnet_rpg.Models.Skill", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("dotnet_rpg.Models.Charecter", b =>
                {
                    b.HasOne("dotnet_rpg.Models.User", "User")
                        .WithMany("Charecters")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Weapon", b =>
                {
                    b.HasOne("dotnet_rpg.Models.Charecter", "Charecter")
                        .WithOne("Weapon")
                        .HasForeignKey("dotnet_rpg.Models.Weapon", "CharecterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Charecter");
                });

            modelBuilder.Entity("dotnet_rpg.Models.Charecter", b =>
                {
                    b.Navigation("Weapon")
                        .IsRequired();
                });

            modelBuilder.Entity("dotnet_rpg.Models.User", b =>
                {
                    b.Navigation("Charecters");
                });
#pragma warning restore 612, 618
        }
    }
}
