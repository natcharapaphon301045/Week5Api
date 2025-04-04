﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Week5.Infrastructure;

#nullable disable

namespace Week5.Migrations
{
    [DbContext(typeof(Week5DbContext))]
    [Migration("20250205103410_FixTableName")]
    partial class FixTableName
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Week5.Domain.BehaviorScore", b =>
                {
                    b.Property<int>("ScoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScoreID"));

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.HasKey("ScoreID");

                    b.HasIndex("StudentID");

                    b.ToTable("BehaviorScores");
                });

            modelBuilder.Entity("Week5.Domain.Class", b =>
                {
                    b.Property<int>("ClassID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClassID"));

                    b.Property<string>("ClassName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClassID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Week5.Domain.Major", b =>
                {
                    b.Property<int>("MajorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MajorID"));

                    b.Property<string>("MajorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MajorID");

                    b.ToTable("Majors");
                });

            modelBuilder.Entity("Week5.Domain.Professor", b =>
                {
                    b.Property<int>("ProfessorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProfessorID"));

                    b.Property<string>("ProfessorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfessorSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProfessorID");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("Week5.Domain.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentID"));

                    b.Property<int>("MajorID")
                        .HasColumnType("int");

                    b.Property<int>("ProfessorID")
                        .HasColumnType("int");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentSurname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentID");

                    b.HasIndex("MajorID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Week5.Domain.StudentClass", b =>
                {
                    b.Property<int>("StudentID")
                        .HasColumnType("int");

                    b.Property<int>("ClassID")
                        .HasColumnType("int");

                    b.Property<int?>("MajorID")
                        .HasColumnType("int");

                    b.Property<int?>("ProfessorID")
                        .HasColumnType("int");

                    b.HasKey("StudentID", "ClassID");

                    b.HasIndex("ClassID");

                    b.HasIndex("MajorID");

                    b.HasIndex("ProfessorID");

                    b.ToTable("StudentClasses");
                });

            modelBuilder.Entity("Week5.Domain.BehaviorScore", b =>
                {
                    b.HasOne("Week5.Domain.Student", "Student")
                        .WithMany("BehaviorScores")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Week5.Domain.Student", b =>
                {
                    b.HasOne("Week5.Domain.Major", "Major")
                        .WithMany()
                        .HasForeignKey("MajorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Week5.Domain.Professor", "Professor")
                        .WithMany()
                        .HasForeignKey("ProfessorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("Professor");
                });

            modelBuilder.Entity("Week5.Domain.StudentClass", b =>
                {
                    b.HasOne("Week5.Domain.Class", "Class")
                        .WithMany("StudentClasses")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Week5.Domain.Major", null)
                        .WithMany("Students")
                        .HasForeignKey("MajorID");

                    b.HasOne("Week5.Domain.Professor", null)
                        .WithMany("Students")
                        .HasForeignKey("ProfessorID");

                    b.HasOne("Week5.Domain.Student", "Student")
                        .WithMany("StudentClasses")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Week5.Domain.Class", b =>
                {
                    b.Navigation("StudentClasses");
                });

            modelBuilder.Entity("Week5.Domain.Major", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Week5.Domain.Professor", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Week5.Domain.Student", b =>
                {
                    b.Navigation("BehaviorScores");

                    b.Navigation("StudentClasses");
                });
#pragma warning restore 612, 618
        }
    }
}
