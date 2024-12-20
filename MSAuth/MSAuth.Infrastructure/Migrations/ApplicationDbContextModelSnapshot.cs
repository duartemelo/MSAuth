﻿// <auto-generated />
using System;
using System.Collections.Generic;
using MSAuth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MSAuth.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MSAuth.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfLastAccess")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfModification")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LockoutEnd")
                        .HasColumnType("datetime2");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RefreshTokenExpire")
                        .HasColumnType("datetime2");

                    b.Property<bool>("TwoFactorActivated")
                        .HasColumnType("bit");

                    b.ComplexProperty<Dictionary<string, object>>("Password", "MSAuth.Domain.Entities.User.Password#Password", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<byte[]>("Hash")
                                .IsRequired()
                                .HasColumnType("varbinary(max)");

                            b1.Property<byte[]>("Salt")
                                .IsRequired()
                                .HasColumnType("varbinary(max)");
                        });

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MSAuth.Domain.Entities.UserConfirmation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("DateOfConfirm")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfExpire")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfModification")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserConfirmations");
                });

            modelBuilder.Entity("MSAuth.Domain.Entities.UserConfirmation", b =>
                {
                    b.HasOne("MSAuth.Domain.Entities.User", "User")
                        .WithMany("UserConfirmations")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MSAuth.Domain.Entities.User", b =>
                {
                    b.Navigation("UserConfirmations");
                });
#pragma warning restore 612, 618
        }
    }
}
