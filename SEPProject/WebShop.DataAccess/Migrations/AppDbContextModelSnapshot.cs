﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShop.DataAccess.WebShopDbContext;

namespace WebShop.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebShop.Core.Model.Accommodation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CostPerNight")
                        .HasColumnType("float");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Accommodations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Address = "Beogradska 14",
                            City = "Belgrade",
                            CostPerNight = 1000.0,
                            Description = "AccomodationDesc",
                            ImagePath = "95024620.jpg",
                            Name = "AccomodationName",
                            OwnerId = new Guid("12345678-1234-1234-1234-123412341234")
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Email = "admin@gmail.com",
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Name = "Admin",
                            Password = "dA0Fz63Kg9DBab5MxlnOgPNgVKjs8TO49jylahDfsCQ=",
                            Salt = "Kee0DHZjSYEgo93KOZtK+g==",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.Conference", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Online")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Conferences");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Address = "Beogradska 14",
                            Date = new DateTime(2022, 1, 28, 14, 40, 47, 57, DateTimeKind.Local).AddTicks(7716),
                            Description = "ConferenceDesc",
                            ImagePath = "conference12345123.jpg",
                            Name = "ConferenceName",
                            Online = false,
                            OwnerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Price = 1400.0
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Online")
                        .HasColumnType("bit");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Address = "Beogradska 14",
                            Description = "CourseDesc",
                            EndDate = new DateTime(2022, 1, 28, 14, 40, 47, 62, DateTimeKind.Local).AddTicks(2697),
                            ImagePath = "conferences-integrated-systems-events-1500x630-2.jpg",
                            Name = "CourseName",
                            Online = false,
                            OwnerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Price = 1400.0,
                            StartDate = new DateTime(2022, 1, 28, 14, 40, 47, 62, DateTimeKind.Local).AddTicks(2668)
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.Item", b =>
                {
                    b.Property<Guid>("ProductKey")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ProductKey");

                    b.HasIndex("OwnerId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            ProductKey = new Guid("12345678-1234-1234-1234-123412341234"),
                            Description = "ItemDesc",
                            ImagePath = "bed.jpg",
                            Name = "ItemName",
                            OwnerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Price = 123.0
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.RegisteredUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ITRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RegisteredUsers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ef5a393a-bd43-4b9b-b2c8-0109859535a2"),
                            AccountNumber = "222222222",
                            Address = "Novosadska 14",
                            Email = "issuer@gmail.com",
                            FirstName = "issuer",
                            ITRole = "default",
                            LastName = "issuer",
                            Password = "GEFBM+uocejlipGWNDkPUbBV3CBb0RGWtY2hY4lXMLs=",
                            PhoneNumber = "123456789",
                            PostalCode = "1234",
                            Salt = "TE/CgC6lKjEWYXPWlC8ymg==",
                            Username = "issuer"
                        },
                        new
                        {
                            Id = new Guid("ea2f1c39-fb77-49ae-a0fc-5b293613d921"),
                            AccountNumber = "333333333",
                            Address = "Beogradska 26",
                            Email = "headOfAcquirement@gmail.com",
                            FirstName = "headOfAcquirement",
                            ITRole = "headOfAcquirement",
                            LastName = "headOfAcquirement",
                            Password = "lZZLNUtNf4+nReyYu6J74nofDChus2IXz6Up8ib1ItM=",
                            PhoneNumber = "123456789",
                            PostalCode = "1234",
                            Salt = "6BxujHVLZV4UGnVfzPXfdw==",
                            Username = "headOfAcquirement"
                        },
                        new
                        {
                            Id = new Guid("ea05bea1-cd06-430c-a74a-c5c960e1863d"),
                            AccountNumber = "444444444",
                            Address = "Novosadska 14",
                            Email = "staff@gmail.com",
                            FirstName = "staff",
                            ITRole = "staff",
                            LastName = "staff",
                            Password = "xpccpvSDpOGhfPAkCRFGsOJsF6qVLH9D7QTNvW48Cpg=",
                            PhoneNumber = "123456789",
                            PostalCode = "1234",
                            Salt = "DMbZWIH2dr64JSmcz4A0WA==",
                            Username = "staff"
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BuyerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("SellerId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            BuyerId = new Guid("ef5a393a-bd43-4b9b-b2c8-0109859535a2"),
                            Currency = "EUR",
                            SellerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Status = 2,
                            Timestamp = new DateTime(2022, 1, 28, 14, 40, 47, 62, DateTimeKind.Local).AddTicks(5599),
                            TotalPrice = 1640.0
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.TransactionItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransactionId");

                    b.ToTable("TransactionItems");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Name = "ItemName",
                            Price = 150.0,
                            ProductId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Quantity = 4,
                            TransactionId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Type = 4
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341235"),
                            Name = "CourseName",
                            Price = 1400.0,
                            ProductId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Quantity = 1,
                            TransactionId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Type = 1
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.Transportation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FinalDestination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("StartDestination")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Transportations");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            DepartureTime = new DateTime(2022, 1, 28, 14, 40, 47, 63, DateTimeKind.Local).AddTicks(528),
                            Description = "TransportationDesc",
                            FinalDestination = "Novi Sad",
                            ImagePath = "conferences-integrated-systems-events-1500x630-2.jpg",
                            Name = "TransportationName",
                            OwnerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Price = 1000.0,
                            StartDestination = "Belgrade"
                        });
                });

            modelBuilder.Entity("WebShop.Core.Model.Accommodation", b =>
                {
                    b.HasOne("WebShop.Core.Model.Admin", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebShop.Core.Model.Conference", b =>
                {
                    b.HasOne("WebShop.Core.Model.Admin", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebShop.Core.Model.Course", b =>
                {
                    b.HasOne("WebShop.Core.Model.Admin", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebShop.Core.Model.Item", b =>
                {
                    b.HasOne("WebShop.Core.Model.Admin", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebShop.Core.Model.Transaction", b =>
                {
                    b.HasOne("WebShop.Core.Model.RegisteredUser", "Buyer")
                        .WithMany()
                        .HasForeignKey("BuyerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebShop.Core.Model.Admin", "Seller")
                        .WithMany()
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Buyer");

                    b.Navigation("Seller");
                });

            modelBuilder.Entity("WebShop.Core.Model.TransactionItem", b =>
                {
                    b.HasOne("WebShop.Core.Model.Transaction", "Transaction")
                        .WithMany("TransactionItems")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("WebShop.Core.Model.Transportation", b =>
                {
                    b.HasOne("WebShop.Core.Model.Admin", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("WebShop.Core.Model.Transaction", b =>
                {
                    b.Navigation("TransactionItems");
                });
#pragma warning restore 612, 618
        }
    }
}
