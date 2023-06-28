﻿// <auto-generated />
using System;
using LibaryWebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LibaryWebApi.Migrations
{
    [DbContext(typeof(SchoolLibraryDbContext))]
    [Migration("20230628081302_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LibaryWebApi.Data.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("BookAuthor")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BookPublisher")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("BookId")
                        .HasName("PK__Books__3DE0C2074E39E7A5");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("LibaryWebApi.Data.Models.BorrowInfo", b =>
                {
                    b.Property<int>("BorrowId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BorrowId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BorrowDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("BorrowId")
                        .HasName("PK__BorrowIn__4295F83FC22FA6C3");

                    b.HasIndex("BookId");

                    b.HasIndex("StudentId");

                    b.ToTable("BorrowInfo", (string)null);
                });

            modelBuilder.Entity("LibaryWebApi.Data.Models.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)");

                    b.HasKey("StudentId")
                        .HasName("PK__Students__32C52B99BE6E1F8A");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LibaryWebApi.Data.Models.BorrowInfo", b =>
                {
                    b.HasOne("LibaryWebApi.Data.Models.Book", "Book")
                        .WithMany("BorrowInfos")
                        .HasForeignKey("BookId")
                        .IsRequired()
                        .HasConstraintName("FK__BorrowInf__BookI__29572725");

                    b.HasOne("LibaryWebApi.Data.Models.Student", "Student")
                        .WithMany("BorrowInfos")
                        .HasForeignKey("StudentId")
                        .IsRequired()
                        .HasConstraintName("FK__BorrowInf__Stude__286302EC");

                    b.Navigation("Book");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("LibaryWebApi.Data.Models.Book", b =>
                {
                    b.Navigation("BorrowInfos");
                });

            modelBuilder.Entity("LibaryWebApi.Data.Models.Student", b =>
                {
                    b.Navigation("BorrowInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
