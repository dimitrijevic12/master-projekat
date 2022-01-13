using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PCC.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TransactionStatus = table.Column<int>(type: "int", nullable: false),
                    AcquirerOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcquirerTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcquirerBankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AcquirerBankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuerBankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuerBankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuerOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssuerTimestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Name", "PAN", "ServerAddress" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "Acquirer bank", "123456", "https://localhost:44375/api/" });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Name", "PAN", "ServerAddress" },
                values: new object[] { new Guid("12345678-1234-1234-1234-222222222222"), "Issuer bank", "222222", "https://localhost:44376/api/" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AcquirerBankId", "AcquirerBankName", "AcquirerOrderId", "AcquirerTimestamp", "Amount", "Currency", "IssuerBankId", "IssuerBankName", "IssuerOrderId", "IssuerTimestamp", "PaymentId", "Timestamp", "TransactionStatus" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341235"), "Acquirer Bank name", new Guid("12345678-1234-1234-1234-123412341235"), new DateTime(2022, 1, 10, 5, 11, 47, 32, DateTimeKind.Local).AddTicks(8522), 444.0, "EUR", new Guid("12345678-1234-1234-1234-123412341234"), "Issuer Bank name", new Guid("12345678-1234-1234-1234-123412341234"), new DateTime(2022, 1, 10, 5, 11, 47, 32, DateTimeKind.Local).AddTicks(8545), new Guid("12345678-1234-1234-1234-123412341234"), new DateTime(2022, 1, 10, 5, 11, 47, 29, DateTimeKind.Local).AddTicks(9611), 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
