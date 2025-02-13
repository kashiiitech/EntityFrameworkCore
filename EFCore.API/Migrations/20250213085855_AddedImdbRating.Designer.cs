﻿// <auto-generated />
using System;
using EFCore.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCore.API.Migrations
{
    [DbContext(typeof(MoviesContext))]
    [Migration("20250213085855_AddedImdbRating")]
    partial class AddedImdbRating
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCore.API.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("EFCore.API.Models.Movie", b =>
                {
                    b.Property<int>("Identifier")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Identifier"));

                    b.Property<int>("AgeRating")
                        .HasColumnType("int");

                    b.Property<int>("ImdbRating")
                        .HasColumnType("int");

                    b.Property<int>("MainGenreId")
                        .HasColumnType("int");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("char(8)");

                    b.Property<string>("Synopsis")
                        .HasColumnType("varchar(max)")
                        .HasColumnName("Plot");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar");

                    b.HasKey("Identifier");

                    b.HasIndex("MainGenreId");

                    b.ToTable("Pictures", (string)null);
                });

            modelBuilder.Entity("EFCore.API.Models.Movie", b =>
                {
                    b.HasOne("EFCore.API.Models.Genre", "Genre")
                        .WithMany("Movies")
                        .HasForeignKey("MainGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("EFCore.API.Models.Person", "Actors", b1 =>
                        {
                            b1.Property<int>("MovieIdentifier")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MovieIdentifier", "Id");

                            b1.ToTable("Pictures_Actors");

                            b1.WithOwner()
                                .HasForeignKey("MovieIdentifier");
                        });

                    b.OwnsOne("EFCore.API.Models.Person", "Director", b1 =>
                        {
                            b1.Property<int>("MovieIdentifier")
                                .HasColumnType("int");

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("MovieIdentifier");

                            b1.ToTable("Pictures_Directors", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("MovieIdentifier");
                        });

                    b.Navigation("Actors");

                    b.Navigation("Director")
                        .IsRequired();

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("EFCore.API.Models.Genre", b =>
                {
                    b.Navigation("Movies");
                });
#pragma warning restore 612, 618
        }
    }
}
