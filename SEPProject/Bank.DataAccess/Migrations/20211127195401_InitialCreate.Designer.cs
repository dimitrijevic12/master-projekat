﻿// <auto-generated />
using System;
using Bank.DataAccess.BankDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20211127195401_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bank.Core.Model.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Accounts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            AccountNumber = "123456789",
                            Balance = 100000.0,
                            UserId = new Guid("12345678-1234-1234-1234-123412341235")
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341235"),
                            AccountNumber = "222222222",
                            Balance = 222222.0,
                            UserId = new Guid("12345678-1234-1234-1234-123412341234")
                        });
                });

            modelBuilder.Entity("Bank.Core.Model.PSPRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("ErrorUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FailedUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MerchantOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("MerchantTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("SuccessUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PSPRequests");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Amount = 123.0,
                            ErrorUrl = "https://www.webshop.com/error",
                            FailedUrl = "https://www.webshop.com/failure",
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341235"),
                            MerchantOrderId = new Guid("12345678-1234-1234-1234-123412341234"),
                            MerchantPassword = "password",
                            MerchantTimestamp = new DateTime(2021, 11, 27, 20, 54, 0, 794, DateTimeKind.Local).AddTicks(8188),
                            SuccessUrl = "https://www.webshop.com/success"
                        });
                });

            modelBuilder.Entity("Bank.Core.Model.PaymentCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CardOwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExpirationDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HolderName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CardOwnerId");

                    b.ToTable("PaymentCards");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            CardOwnerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            ExpirationDate = "4/22",
                            HolderName = "Holder Name",
                            PAN = "123456789",
                            SecurityCode = "1234"
                        });
                });

            modelBuilder.Entity("Bank.Core.Model.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcquirerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcquirerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<Guid>("IssuerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IssuerName")
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
                            AcquirerId = new Guid("12345678-1234-1234-1234-123412341235"),
                            AcquirerName = "Acquirer name",
                            Amount = 444.0,
                            IssuerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            IssuerName = "Issuer name",
                            OrderId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Timestamp = new DateTime(2021, 11, 27, 20, 54, 0, 799, DateTimeKind.Local).AddTicks(8085),
                            TransactionStatus = 1
                        });
                });

            modelBuilder.Entity("Bank.Core.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("Bank.Core.Model.Merchant", b =>
                {
                    b.HasBaseType("Bank.Core.Model.User");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Merchant");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341235"),
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341232"),
                            MerchantPassword = "password"
                        });
                });

            modelBuilder.Entity("Bank.Core.Model.RegisteredUser", b =>
                {
                    b.HasBaseType("Bank.Core.Model.User");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UniquePersonalRegistrationNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("RegisteredUser");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            DateOfBirth = new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailAddress = "user@gmail.com",
                            FirstName = "FirstName",
                            LastName = "LastName",
                            UniquePersonalRegistrationNumber = "123456789"
                        });
                });

            modelBuilder.Entity("Bank.Core.Model.Account", b =>
                {
                    b.HasOne("Bank.Core.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bank.Core.Model.PaymentCard", b =>
                {
                    b.HasOne("Bank.Core.Model.RegisteredUser", "CardOwner")
                        .WithMany("PaymentCards")
                        .HasForeignKey("CardOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardOwner");
                });

            modelBuilder.Entity("Bank.Core.Model.RegisteredUser", b =>
                {
                    b.Navigation("PaymentCards");
                });
#pragma warning restore 612, 618
        }
    }
}
