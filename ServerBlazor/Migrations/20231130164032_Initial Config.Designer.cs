﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServerBlazor.Data;

#nullable disable

namespace ServerBlazor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231130164032_Initial Config")]
    partial class InitialConfig
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ServerBlazor.Models.Entities.Blog", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BlogId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BlogId");

                    b.ToTable("Blog", (string)null);

                    b.HasData(
                        new
                        {
                            BlogId = 1,
                            Name = "Fisking"
                        },
                        new
                        {
                            BlogId = 2,
                            Name = "Klatring"
                        });
                });

            modelBuilder.Entity("ServerBlazor.Models.Entities.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommentId"), 1L, 1);

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comment", (string)null);

                    b.HasData(
                        new
                        {
                            CommentId = 1,
                            CommentText = "Så kult, tenker jeg også må ta turen dit!",
                            PostId = 1
                        },
                        new
                        {
                            CommentId = 2,
                            CommentText = "Hvordan var parkeringen ved elva?",
                            PostId = 1
                        },
                        new
                        {
                            CommentId = 3,
                            CommentText = "Har sendt direktemelding",
                            PostId = 2
                        },
                        new
                        {
                            CommentId = 4,
                            CommentText = "Klarer ikke vente!",
                            PostId = 3
                        },
                        new
                        {
                            CommentId = 5,
                            CommentText = "Det gledes!",
                            PostId = 3
                        });
                });

            modelBuilder.Entity("ServerBlazor.Models.Entities.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"), 1L, 1);

                    b.Property<int>("BlogId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PostId");

                    b.HasIndex("BlogId");

                    b.ToTable("Post", (string)null);

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            BlogId = 1,
                            Content = "Kort historie om fisking i Straumen",
                            Title = "Fisking i Straumen"
                        },
                        new
                        {
                            PostId = 2,
                            BlogId = 1,
                            Content = "Kontakt meg for mer info",
                            Title = "Fiskekort til salgs"
                        },
                        new
                        {
                            PostId = 3,
                            BlogId = 2,
                            Content = "Arrangeres Sommeren 2048",
                            Title = "Klatre Konkurranse i Narvik"
                        });
                });

            modelBuilder.Entity("ServerBlazor.Models.Entities.Comment", b =>
                {
                    b.HasOne("ServerBlazor.Models.Entities.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });

            modelBuilder.Entity("ServerBlazor.Models.Entities.Post", b =>
                {
                    b.HasOne("ServerBlazor.Models.Entities.Blog", "Blog")
                        .WithMany("Posts")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Blog");
                });

            modelBuilder.Entity("ServerBlazor.Models.Entities.Blog", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("ServerBlazor.Models.Entities.Post", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
