﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using SilverBotDS.Objects;

namespace SilverBotDS.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210512081422_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0-preview.3.21201.2");

            modelBuilder.Entity("SilverBotDS.Objects.Database.Classes.ServerStatString", b =>
                {
                    b.Property<ulong>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<ulong?>("ServerSettingsServerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Template")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.HasIndex("ServerSettingsServerId");

                    b.ToTable("ServerStatString");
                });

            modelBuilder.Entity("SilverBotDS.Objects.ServerSettings", b =>
                {
                    b.Property<ulong>("ServerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("EmotesOptin")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LangName")
                        .HasColumnType("TEXT");

                    b.Property<ulong?>("ServerStatsCategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ServerId");

                    b.ToTable("serverSettings");
                });

            modelBuilder.Entity("SilverBotDS.Objects.UserSettings", b =>
                {
                    b.Property<ulong>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBanned")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LangName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("userSettings");
                });

            modelBuilder.Entity("SilverBotDS.Objects.Database.Classes.ServerStatString", b =>
                {
                    b.HasOne("SilverBotDS.Objects.ServerSettings", null)
                        .WithMany("ServerStatsTemplates")
                        .HasForeignKey("ServerSettingsServerId");
                });

            modelBuilder.Entity("SilverBotDS.Objects.ServerSettings", b =>
                {
                    b.Navigation("ServerStatsTemplates");
                });
#pragma warning restore 612, 618
        }
    }
}
