﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PSPRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    MerchantOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuccessUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FailedUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSPRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    AcquirerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcquirerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    MerchantPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UniquePersonalRegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpirationDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CardOwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentCards_User_CardOwnerId",
                        column: x => x.CardOwnerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PSPRequests",
                columns: new[] { "Id", "Amount", "ErrorUrl", "FailedUrl", "MerchantId", "MerchantOrderId", "MerchantPassword", "MerchantTimestamp", "SuccessUrl" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), 123.0, "https://www.webshop.com/error", "https://www.webshop.com/failure", new Guid("12345678-1234-1234-1234-123412341235"), new Guid("12345678-1234-1234-1234-123412341234"), "password", new DateTime(2021, 11, 27, 20, 54, 0, 794, DateTimeKind.Local).AddTicks(8188), "https://www.webshop.com/success" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AcquirerId", "AcquirerName", "Amount", "IssuerId", "IssuerName", "OrderId", "Timestamp", "TransactionStatus" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341235"), "Acquirer name", 444.0, new Guid("12345678-1234-1234-1234-123412341234"), "Issuer name", new Guid("12345678-1234-1234-1234-123412341234"), new DateTime(2021, 11, 27, 20, 54, 0, 799, DateTimeKind.Local).AddTicks(8085), 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Discriminator", "MerchantId", "MerchantPassword" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341235"), "Merchant", new Guid("12345678-1234-1234-1234-123412341232"), "password" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateOfBirth", "Discriminator", "EmailAddress", "FirstName", "LastName", "UniquePersonalRegistrationNumber" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "RegisteredUser", "user@gmail.com", "FirstName", "LastName", "123456789" });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "UserId" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "123456789", 100000.0, new Guid("12345678-1234-1234-1234-123412341235") });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "UserId" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341235"), "222222222", 222222.0, new Guid("12345678-1234-1234-1234-123412341234") });

            migrationBuilder.InsertData(
                table: "PaymentCards",
                columns: new[] { "Id", "CardOwnerId", "ExpirationDate", "HolderName", "PAN", "SecurityCode" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341234"), "4/22", "Holder Name", "123456789", "1234" });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCards_CardOwnerId",
                table: "PaymentCards",
                column: "CardOwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PaymentCards");

            migrationBuilder.DropTable(
                name: "PSPRequests");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
