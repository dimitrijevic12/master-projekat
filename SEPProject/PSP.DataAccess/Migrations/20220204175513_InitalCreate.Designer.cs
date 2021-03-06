// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PSP.DataAccess.PSPDbContext;

namespace PSP.DataAccess.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220204175513_InitalCreate")]
    partial class InitalCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PSP.Core.Model.Merchant", b =>
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123422941234"),
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341234"),
                            MerchantPassword = "password",
                            Name = "Name",
                            RegisteredWebShopId = new Guid("12345678-1234-1234-1234-123412341230")
                        });
                });

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
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-223412341234"),
                            Name = "CryptoValute"
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-323412341234"),
                            Name = "Bank"
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
                        },
                        new
                        {
                            PaymentTypeId = new Guid("12345678-1234-1234-1234-223412341234"),
                            RegisteredWebShopId = new Guid("12345678-1234-1234-1234-123412341230")
                        },
                        new
                        {
                            PaymentTypeId = new Guid("12345678-1234-1234-1234-323412341234"),
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341230"),
                            EmailAddress = "gmail@gmail.com",
                            ErrorUrl = "https://172.20.10.2:3000/error-transaction",
                            FailedUrl = "https://172.20.10.2:3000/failed-transaction",
                            Password = "Vw73lwyE0LkxR3qGNGGefU2/9n6KmuyK68RHbcIlkBM=",
                            Salt = "DhR9MbXejS+TQxW3rvMT1g==",
                            SuccessUrl = "https://172.20.10.2:3000/perdiem-transaction",
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Amount = 100.0,
                            Currency = "EUR",
                            IssuerId = new Guid("12345678-1234-1234-1234-123412341235"),
                            IssuerName = "IssuerName",
                            MerchantId = new Guid("12345678-1234-1234-1234-123412341233"),
                            MerchantName = "MerchantName",
                            OrderId = new Guid("12345678-1234-1234-1234-123412341232"),
                            PaymentId = new Guid("00000000-0000-0000-0000-000000000000"),
                            Timestamp = new DateTime(2022, 2, 4, 18, 55, 12, 447, DateTimeKind.Local).AddTicks(5916),
                            TransactionStatus = 0,
                            Type = "Other"
                        });
                });

            modelBuilder.Entity("PSP.Core.Model.Merchant", b =>
                {
                    b.HasOne("PSP.Core.Model.RegisteredWebShop", "RegisteredWebShop")
                        .WithMany("Merchant")
                        .HasForeignKey("RegisteredWebShopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RegisteredWebShop");
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
                    b.Navigation("Merchant");

                    b.Navigation("PaymentTypeRegisteredWebShops");
                });
#pragma warning restore 612, 618
        }
    }
}
