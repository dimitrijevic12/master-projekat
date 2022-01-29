﻿// <auto-generated />
using System;
using IssuerBank.DataAccess.BankDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IssuerBank.DataAccess.Migrations
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

            modelBuilder.Entity("IssuerBank.Core.Model.Account", b =>
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
                            AccountNumber = "123456781",
                            Balance = 100000.0,
                            UserId = new Guid("12345678-1234-1234-1234-123412341235")
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341235"),
                            AccountNumber = "222222221",
                            Balance = 222222.0,
                            UserId = new Guid("12345678-1234-1234-1234-123412341234")
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341236"),
                            AccountNumber = "333333331",
                            Balance = 222222.0,
                            UserId = new Guid("d969bb55-393a-4b22-9507-f4b492b3413f")
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341237"),
                            AccountNumber = "444444441",
                            Balance = 222222.0,
                            UserId = new Guid("665166bf-411c-4ba9-a16d-2a6460a59500")
                        });
                });

            modelBuilder.Entity("IssuerBank.Core.Model.PSPRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

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
                            Currency = "EUR",
                            ErrorUrl = "https://www.webshop.com/error",
                            FailedUrl = "https://www.webshop.com/failure",
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341235"),
                            MerchantOrderId = new Guid("12345678-1234-1234-1234-123412341234"),
                            MerchantPassword = "password",
                            MerchantTimestamp = new DateTime(2022, 1, 29, 15, 16, 14, 471, DateTimeKind.Local).AddTicks(424),
                            SuccessUrl = "https://www.webshop.com/success"
                        });
                });

            modelBuilder.Entity("IssuerBank.Core.Model.PSPResponse", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PSPRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PaymentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PaymentUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PSPRequestId");

                    b.ToTable("PSPResponse");
                });

            modelBuilder.Entity("IssuerBank.Core.Model.PaymentCard", b =>
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
                            ExpirationDate = "04/22",
                            HolderName = "Holder Name",
                            PAN = "2222221234561234",
                            SecurityCode = "1234"
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341235"),
                            CardOwnerId = new Guid("d969bb55-393a-4b22-9507-f4b492b3413f"),
                            ExpirationDate = "04/22",
                            HolderName = "Acquirer Name",
                            PAN = "2222222222221234",
                            SecurityCode = "1234"
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341236"),
                            CardOwnerId = new Guid("665166bf-411c-4ba9-a16d-2a6460a59500"),
                            ExpirationDate = "04/22",
                            HolderName = "Staff Name",
                            PAN = "2222223333331234",
                            SecurityCode = "1234"
                        });
                });

            modelBuilder.Entity("IssuerBank.Core.Model.Transaction", b =>
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

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IssuerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IssuerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PaymentId")
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
                            Currency = "EUR",
                            IssuerId = new Guid("12345678-1234-1234-1234-123412341234"),
                            IssuerName = "Issuer name",
                            PaymentId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Timestamp = new DateTime(2022, 1, 29, 15, 16, 14, 502, DateTimeKind.Local).AddTicks(4473),
                            TransactionStatus = 1
                        });
                });

            modelBuilder.Entity("IssuerBank.Core.Model.User", b =>
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

            modelBuilder.Entity("IssuerBank.Core.Model.Merchant", b =>
                {
                    b.HasBaseType("IssuerBank.Core.Model.User");

                    b.Property<Guid>("MerchantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("MerchantPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Merchant");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341235"),
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341234"),
                            MerchantPassword = "password",
                            Name = "Merchant name"
                        });
                });

            modelBuilder.Entity("IssuerBank.Core.Model.RegisteredUser", b =>
                {
                    b.HasBaseType("IssuerBank.Core.Model.User");

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
                        },
                        new
                        {
                            Id = new Guid("d969bb55-393a-4b22-9507-f4b492b3413f"),
                            DateOfBirth = new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailAddress = "headOfAcquirement@gmail.com",
                            FirstName = "headOfAcquirement",
                            LastName = "headOfAcquirement",
                            UniquePersonalRegistrationNumber = "123456790"
                        },
                        new
                        {
                            Id = new Guid("665166bf-411c-4ba9-a16d-2a6460a59500"),
                            DateOfBirth = new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            EmailAddress = "staff@gmail.com",
                            FirstName = "staff",
                            LastName = "staff",
                            UniquePersonalRegistrationNumber = "123456791"
                        });
                });

            modelBuilder.Entity("IssuerBank.Core.Model.Account", b =>
                {
                    b.HasOne("IssuerBank.Core.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("IssuerBank.Core.Model.PSPResponse", b =>
                {
                    b.HasOne("IssuerBank.Core.Model.PSPRequest", "PSPRequest")
                        .WithMany()
                        .HasForeignKey("PSPRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PSPRequest");
                });

            modelBuilder.Entity("IssuerBank.Core.Model.PaymentCard", b =>
                {
                    b.HasOne("IssuerBank.Core.Model.RegisteredUser", "CardOwner")
                        .WithMany("PaymentCards")
                        .HasForeignKey("CardOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CardOwner");
                });

            modelBuilder.Entity("IssuerBank.Core.Model.RegisteredUser", b =>
                {
                    b.Navigation("PaymentCards");
                });
#pragma warning restore 612, 618
        }
    }
}
