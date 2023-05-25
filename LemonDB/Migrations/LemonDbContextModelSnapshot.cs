﻿// <auto-generated />
using System;
using LemonDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LemonDB.Migrations
{
    [DbContext(typeof(LemonDbContext))]
    partial class LemonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccountSession", b =>
                {
                    b.Property<Guid>("ParticipantsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SessionsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ParticipantsId", "SessionsId");

                    b.HasIndex("SessionsId");

                    b.ToTable("AccountSession");
                });

            modelBuilder.Entity("AccountStuff", b =>
                {
                    b.Property<Guid>("AccountsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StuffsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AccountsId", "StuffsId");

                    b.HasIndex("StuffsId");

                    b.ToTable("AccountStuff");
                });

            modelBuilder.Entity("LemonDB.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("LemonDB.AccountStatistic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Deaths")
                        .HasColumnType("int");

                    b.Property<double>("Exp")
                        .HasColumnType("float");

                    b.Property<int>("Plays")
                        .HasColumnType("int");

                    b.Property<int>("Wins")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("AccountStatistic");
                });

            modelBuilder.Entity("LemonDB.Cash", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Current")
                        .HasColumnType("float");

                    b.Property<double>("TotalHistory")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Cashs");
                });

            modelBuilder.Entity("LemonDB.Map", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxPlayersCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Maps");
                });

            modelBuilder.Entity("LemonDB.PlayerSessionStat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Coins")
                        .HasColumnType("int");

                    b.Property<double>("DeadTimePoint")
                        .HasColumnType("float");

                    b.Property<double>("Exp")
                        .HasColumnType("float");

                    b.Property<int>("Fails")
                        .HasColumnType("int");

                    b.Property<int>("Punches")
                        .HasColumnType("int");

                    b.Property<int>("Rank")
                        .HasColumnType("int");

                    b.Property<Guid>("SessionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("SpawnTimePoint")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("SessionId");

                    b.ToTable("PlayersSessionsStats");
                });

            modelBuilder.Entity("LemonDB.Session", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("GameKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("MapId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StartPlayersCount")
                        .HasColumnType("int");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MapId");

                    b.ToTable("Sessions");
                });

            modelBuilder.Entity("LemonDB.Stuff", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GameKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stuffs");
                });

            modelBuilder.Entity("AccountSession", b =>
                {
                    b.HasOne("LemonDB.Account", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LemonDB.Session", null)
                        .WithMany()
                        .HasForeignKey("SessionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AccountStuff", b =>
                {
                    b.HasOne("LemonDB.Account", null)
                        .WithMany()
                        .HasForeignKey("AccountsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LemonDB.Stuff", null)
                        .WithMany()
                        .HasForeignKey("StuffsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LemonDB.AccountStatistic", b =>
                {
                    b.HasOne("LemonDB.Account", null)
                        .WithOne("Statistic")
                        .HasForeignKey("LemonDB.AccountStatistic", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LemonDB.Cash", b =>
                {
                    b.HasOne("LemonDB.Account", null)
                        .WithOne("Cash")
                        .HasForeignKey("LemonDB.Cash", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LemonDB.PlayerSessionStat", b =>
                {
                    b.HasOne("LemonDB.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LemonDB.Session", "Session")
                        .WithMany()
                        .HasForeignKey("SessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Session");
                });

            modelBuilder.Entity("LemonDB.Session", b =>
                {
                    b.HasOne("LemonDB.Map", "Map")
                        .WithMany()
                        .HasForeignKey("MapId");

                    b.Navigation("Map");
                });

            modelBuilder.Entity("LemonDB.Account", b =>
                {
                    b.Navigation("Cash")
                        .IsRequired();

                    b.Navigation("Statistic")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
