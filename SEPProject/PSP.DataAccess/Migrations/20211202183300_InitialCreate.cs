﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PSP.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredWebShops",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebShopId = table.Column<int>(type: "int", nullable: false),
                    WebShopName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SuccessUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FailedUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErrorUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredWebShops", x => x.Id);
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
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuerName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredWebShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Merchants_RegisteredWebShops_RegisteredWebShopId",
                        column: x => x.RegisteredWebShopId,
                        principalTable: "RegisteredWebShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypeRegisteredWebShop",
                columns: table => new
                {
                    PaymentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RegisteredWebShopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeRegisteredWebShop", x => new { x.PaymentTypeId, x.RegisteredWebShopId });
                    table.ForeignKey(
                        name: "FK_PaymentTypeRegisteredWebShop_PaymentTypes_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "PaymentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentTypeRegisteredWebShop_RegisteredWebShops_RegisteredWebShopId",
                        column: x => x.RegisteredWebShopId,
                        principalTable: "RegisteredWebShops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), "PayPal" },
                    { new Guid("12345678-1234-1234-1234-223412341234"), "CryptoValute" },
                    { new Guid("12345678-1234-1234-1234-323412341234"), "Bank" }
                });

            migrationBuilder.InsertData(
                table: "RegisteredWebShops",
                columns: new[] { "Id", "EmailAddress", "ErrorUrl", "FailedUrl", "Password", "SuccessUrl", "WebShopId", "WebShopName" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341230"), "gmail@gmail.com", "http://farm4.static.flickr.com/2232/2232/someimage.jpg", "http://farm4.static.flickr.com/2232/2232/someimage.jpg", "password", "http://farm4.static.flickr.com/2232/2232/someimage.jpg", 123, "WebShopName" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "IssuerId", "IssuerName", "MerchantId", "MerchantName", "OrderId", "Timestamp", "TransactionStatus" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), 100.0, new Guid("12345678-1234-1234-1234-123412341235"), "IssuerName", new Guid("12345678-1234-1234-1234-123412341233"), "MerchantName", new Guid("12345678-1234-1234-1234-123412341232"), new DateTime(2021, 12, 2, 19, 32, 59, 680, DateTimeKind.Local).AddTicks(4966), 0 });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "Id", "MerchantId", "MerchantPassword", "Name", "RegisteredWebShopId" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123422941234"), new Guid("12345678-1234-1234-1234-123422641234"), "Password", "Name", new Guid("12345678-1234-1234-1234-123412341230") });

            migrationBuilder.InsertData(
                table: "PaymentTypeRegisteredWebShop",
                columns: new[] { "PaymentTypeId", "RegisteredWebShopId" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341230") },
                    { new Guid("12345678-1234-1234-1234-223412341234"), new Guid("12345678-1234-1234-1234-123412341230") },
                    { new Guid("12345678-1234-1234-1234-323412341234"), new Guid("12345678-1234-1234-1234-123412341230") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_RegisteredWebShopId",
                table: "Merchants",
                column: "RegisteredWebShopId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypeRegisteredWebShop_RegisteredWebShopId",
                table: "PaymentTypeRegisteredWebShop",
                column: "RegisteredWebShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "PaymentTypeRegisteredWebShop");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "RegisteredWebShops");
        }
    }
}