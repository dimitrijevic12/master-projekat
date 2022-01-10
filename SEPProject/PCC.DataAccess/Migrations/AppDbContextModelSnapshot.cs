﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PCC.DataAccess.PCCDbContext;

namespace PCC.DataAccess.Migrations
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

            modelBuilder.Entity("PCC.Core.Model.Bank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServerAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Banks");

                    b.HasData(
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-123412341234"),
                            Name = "Acquirer bank",
                            PAN = "123456",
                            ServerAddress = "https://localhost:44375/api/"
                        },
                        new
                        {
                            Id = new Guid("12345678-1234-1234-1234-222222222222"),
                            Name = "Issuer bank",
                            PAN = "222222",
                            ServerAddress = "https://localhost:44376/api/"
                        });
                });

            modelBuilder.Entity("PCC.Core.Model.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AcquirerBankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AcquirerBankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("AcquirerOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("AcquirerTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IssuerBankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IssuerBankName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IssuerOrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("IssuerTimestamp")
                        .HasColumnType("datetime2");

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
                            AcquirerBankId = new Guid("12345678-1234-1234-1234-123412341235"),
                            AcquirerBankName = "Acquirer Bank name",
                            AcquirerOrderId = new Guid("12345678-1234-1234-1234-123412341235"),
                            AcquirerTimestamp = new DateTime(2022, 1, 10, 5, 11, 47, 32, DateTimeKind.Local).AddTicks(8522),
                            Amount = 444.0,
                            Currency = "EUR",
                            IssuerBankId = new Guid("12345678-1234-1234-1234-123412341234"),
                            IssuerBankName = "Issuer Bank name",
                            IssuerOrderId = new Guid("12345678-1234-1234-1234-123412341234"),
                            IssuerTimestamp = new DateTime(2022, 1, 10, 5, 11, 47, 32, DateTimeKind.Local).AddTicks(8545),
                            PaymentId = new Guid("12345678-1234-1234-1234-123412341234"),
                            Timestamp = new DateTime(2022, 1, 10, 5, 11, 47, 29, DateTimeKind.Local).AddTicks(9611),
                            TransactionStatus = 1
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
