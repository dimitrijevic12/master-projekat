﻿// <auto-generated />
using System;
using CardPayment.DataAccess.CardPaymentDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CardPayment.DataAccess.Migrations
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

            modelBuilder.Entity("CardPayment.Core.Model.Merchant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RegisteredWebShopId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RegisteredWebShopId");

                    b.ToTable("Merchants");
                });

            modelBuilder.Entity("CardPayment.Core.Model.PaymentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("CardPayment.Core.Model.PaymentTypeRegisteredWebShop", b =>
                {
                    b.Property<Guid>("PaymentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RegisteredWebShopId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("PaymentTypeId", "RegisteredWebShopId");

                    b.HasIndex("RegisteredWebShopId");

                    b.ToTable("PaymentTypeRegisteredWebShop");
                });

            modelBuilder.Entity("CardPayment.Core.Model.RegisteredWebShop", b =>
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

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SuccessUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WebShopId")
                        .HasColumnType("int");

                    b.Property<string>("WebShopName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RegisteredWebShops");
                });

            modelBuilder.Entity("CardPayment.Core.Model.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

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

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionStatus")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("CardPayment.Core.Model.Merchant", b =>
                {
                    b.HasOne("CardPayment.Core.Model.RegisteredWebShop", "RegisteredWebShop")
                        .WithMany("Merchant")
                        .HasForeignKey("RegisteredWebShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegisteredWebShop");
                });

            modelBuilder.Entity("CardPayment.Core.Model.PaymentTypeRegisteredWebShop", b =>
                {
                    b.HasOne("CardPayment.Core.Model.PaymentType", "PaymentType")
                        .WithMany("PaymentTypeRegisteredWebShops")
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CardPayment.Core.Model.RegisteredWebShop", "RegisteredWebShop")
                        .WithMany("PaymentTypeRegisteredWebShops")
                        .HasForeignKey("RegisteredWebShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PaymentType");

                    b.Navigation("RegisteredWebShop");
                });

            modelBuilder.Entity("CardPayment.Core.Model.PaymentType", b =>
                {
                    b.Navigation("PaymentTypeRegisteredWebShops");
                });

            modelBuilder.Entity("CardPayment.Core.Model.RegisteredWebShop", b =>
                {
                    b.Navigation("Merchant");

                    b.Navigation("PaymentTypeRegisteredWebShops");
                });
#pragma warning restore 612, 618
        }
    }
}
