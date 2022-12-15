﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PavlovStatsReader;

#nullable disable

namespace PavlovStatsReader.Migrations
{
    [DbContext(typeof(StatsContext))]
    partial class StatsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PavlovStatsReader.Models.BombData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("BombInteraction")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("BombInteraction");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<ulong>("Player")
                        .HasColumnType("bigint unsigned")
                        .HasColumnName("Player");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.HasKey("Id");

                    b.ToTable("BombData");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.EndOfMapStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("GameMode")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("GameMode");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("MapLabel")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("MapLabel");

                    b.Property<int>("PlayerCount")
                        .HasColumnType("int")
                        .HasColumnName("PlayerCount");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.Property<int>("Team0Score")
                        .HasColumnType("int")
                        .HasColumnName("Team0Score");

                    b.Property<int>("Team1Score")
                        .HasColumnType("int")
                        .HasColumnName("Team1Score");

                    b.Property<bool>("Teams")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Teams");

                    b.HasKey("Id");

                    b.ToTable("EndOfMapStats");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.KillData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Headshot")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("Headshot");

                    b.Property<ulong>("Killed")
                        .HasColumnType("bigint unsigned")
                        .HasColumnName("Killed");

                    b.Property<string>("KilledBy")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("KilledBy");

                    b.Property<int>("KilledTeamID")
                        .HasColumnType("int")
                        .HasColumnName("KilledTeamID");

                    b.Property<ulong>("Killer")
                        .HasColumnType("bigint unsigned")
                        .HasColumnName("Killer");

                    b.Property<int>("KillerTeamID")
                        .HasColumnType("int")
                        .HasColumnName("KillerTeamID");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.HasKey("Id");

                    b.ToTable("KillData");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.PlayerStats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("EndOfMapStatsId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("PlayerName");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.Property<int>("TeamId")
                        .HasColumnType("int")
                        .HasColumnName("TeamId");

                    b.Property<ulong>("UniqueId")
                        .HasColumnType("bigint unsigned")
                        .HasColumnName("UniqueId");

                    b.HasKey("Id");

                    b.HasIndex("EndOfMapStatsId");

                    b.ToTable("PlayerStats");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.RoundEnd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Round")
                        .HasColumnType("int")
                        .HasColumnName("Round");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.Property<int>("WinningTeam")
                        .HasColumnType("int")
                        .HasColumnName("WinningTeam");

                    b.HasKey("Id");

                    b.ToTable("RoundEnds");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.RoundState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("State");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("RoundStates");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.Setting", b =>
                {
                    b.Property<string>("ServerId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("ServerId", "Name");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.Stats", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("PlayerStatsId")
                        .HasColumnType("int");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.Property<string>("StatType")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("StatType");

                    b.HasKey("Id");

                    b.HasIndex("PlayerStatsId");

                    b.ToTable("Stats");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.SwitchTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LogEntryDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("NewTeamID")
                        .HasColumnType("int")
                        .HasColumnName("NewTeamID");

                    b.Property<ulong>("PlayerID")
                        .HasColumnType("bigint unsigned")
                        .HasColumnName("PlayerID");

                    b.Property<string>("ServerId")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("ServerId");

                    b.HasKey("Id");

                    b.ToTable("SwitchTeams");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.PlayerStats", b =>
                {
                    b.HasOne("PavlovStatsReader.Models.EndOfMapStats", "EndOfMapStats")
                        .WithMany("PlayerStats")
                        .HasForeignKey("EndOfMapStatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EndOfMapStats");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.Stats", b =>
                {
                    b.HasOne("PavlovStatsReader.Models.PlayerStats", "PlayerStats")
                        .WithMany("Stats")
                        .HasForeignKey("PlayerStatsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PlayerStats");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.EndOfMapStats", b =>
                {
                    b.Navigation("PlayerStats");
                });

            modelBuilder.Entity("PavlovStatsReader.Models.PlayerStats", b =>
                {
                    b.Navigation("Stats");
                });
#pragma warning restore 612, 618
        }
    }
}