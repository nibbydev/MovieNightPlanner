﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(DbContext))]
    [Migration("20190410181537_first")]
    partial class first
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("DAL.Domain.Submission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddedBy")
                        .HasMaxLength(32);

                    b.Property<string>("Duration");

                    b.Property<int?>("Episodes");

                    b.Property<string>("Genres");

                    b.Property<string>("ImageUrl");

                    b.Property<int?>("MalId");

                    b.Property<string>("Rating");

                    b.Property<double?>("Score");

                    b.Property<string>("Synopsis");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Title");

                    b.Property<string>("TrailerUrl");

                    b.Property<string>("Type");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Submissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddedBy = "catnib",
                            ImageUrl = "https://cdn.myanimelist.net/images/anime/12/76049.jpg",
                            Time = new DateTime(2019, 4, 10, 21, 15, 36, 888, DateTimeKind.Local).AddTicks(4595),
                            Title = "One Punch Man",
                            Url = "https://myanimelist.net/anime/30276/One_Punch_Man"
                        },
                        new
                        {
                            Id = 2,
                            AddedBy = "siegrest",
                            ImageUrl = "https://cdn.myanimelist.net/images/anime/3/77176.jpg",
                            Time = new DateTime(2019, 4, 10, 21, 15, 36, 889, DateTimeKind.Local).AddTicks(9398),
                            Title = "Mobile Suit Gundam Thunderbolt",
                            Url = "https://myanimelist.net/anime/31973/Mobile_Suit_Gundam_Thunderbolt"
                        },
                        new
                        {
                            Id = 3,
                            AddedBy = "rinnex",
                            ImageUrl = "https://cdn.myanimelist.net/images/anime/1562/100460.jpg",
                            Time = new DateTime(2019, 4, 10, 21, 15, 36, 889, DateTimeKind.Local).AddTicks(9417),
                            Title = "Fairy Gone",
                            Url = "https://myanimelist.net/anime/39063/Fairy_Gone"
                        });
                });

            modelBuilder.Entity("DAL.Domain.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Ip");

                    b.Property<int>("SubmissionId");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<bool>("Value");

                    b.HasKey("Id");

                    b.HasIndex("SubmissionId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("DAL.Domain.Vote", b =>
                {
                    b.HasOne("DAL.Domain.Submission", "Submission")
                        .WithMany()
                        .HasForeignKey("SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
