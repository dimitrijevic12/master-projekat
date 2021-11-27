﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSP.DataAccess.PSPDbContext;

namespace PSP.DataAccess.Migrations
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

            modelBuilder.Entity("PSP.Core.Model.PaymentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Name = "PayPal"
                        });
                });

            modelBuilder.Entity("PSP.Core.Model.PaymentTypeRegisteredWebShop", b =>
                {
                    b.Property<Guid>("PaymentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RegisteredWebShopId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PaymentTypeId", "RegisteredWebShopId");

                    b.HasIndex("RegisteredWebShopId");

                    b.ToTable("PaymentTypeRegisteredWebShop");

                    b.HasData(
                        new
                        {
                            PaymentTypeId = new Guid("12345678-1234-1234-1234-123412341234"),
                            RegisteredWebShopId = new Guid("12345678-1234-1234-1234-123412341230")
                        });
                });

            modelBuilder.Entity("PSP.Core.Model.RegisteredWebShop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ErrorUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FailedUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SuccessUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WebShopId")
                        .HasColumnType("int");

                    b.Property<string>("WebShopName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RegisteredWebShops");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341230"),
                            EmailAddress = "gmail@gmail.com",
                            ErrorUrl = "http://farm4.static.flickr.com/2232/2232/someimage.jpg",
                            FailedUrl = "http://farm4.static.flickr.com/2232/2232/someimage.jpg",
                            Password = "password",
                            SuccessUrl = "http://farm4.static.flickr.com/2232/2232/someimage.jpg",
                            WebShopId = 123,
                            WebShopName = "WebShopName"
                        });
                });

            modelBuilder.Entity("PSP.Core.Model.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid>("IssuerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IssuerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Amount = 100.0,
                            IssuerId = new Guid("12345678-1234-1234-1234-123412341235"),
                            IssuerName = "IssuerName",
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341233"),
                            MerchantName = "MerchantName",
                            OrderId = new Guid("12345678-1234-1234-1234-123412341232"),
                            Timestamp = new DateTime(2021, 11, 27, 16, 39, 18, 338, DateTimeKind.Local).AddTicks(2169),
                            TransactionStatus = 0
                        });
                });

            modelBuilder.Entity("PSP.Core.Model.PaymentTypeRegisteredWebShop", b =>
                {
                    b.HasOne("PSP.Core.Model.PaymentType", "PaymentType")
                        .WithMany("PaymentTypeRegisteredWebShops")
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PSP.Core.Model.RegisteredWebShop", "RegisteredWebShop")
                        .WithMany("PaymentTypeRegisteredWebShops")
                        .HasForeignKey("RegisteredWebShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentType");

                    b.Navigation("RegisteredWebShop");
                });

            modelBuilder.Entity("PSP.Core.Model.PaymentType", b =>
                {
                    b.Navigation("PaymentTypeRegisteredWebShops");
                });

            modelBuilder.Entity("PSP.Core.Model.RegisteredWebShop", b =>
                {
                    b.Navigation("PaymentTypeRegisteredWebShops");
                });
#pragma warning restore 612, 618
        }
    }
}
