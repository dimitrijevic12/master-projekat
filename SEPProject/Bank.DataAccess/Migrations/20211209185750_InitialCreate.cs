using System;
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
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "PSPResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PSPRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PSPResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PSPResponse_PSPRequests_PSPRequestId",
                        column: x => x.PSPRequestId,
                        principalTable: "PSPRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), 123.0, "https://www.webshop.com/error", "https://www.webshop.com/failure", new Guid("12345678-1234-1234-1234-123412341235"), new Guid("12345678-1234-1234-1234-123412341234"), "password", new DateTime(2021, 12, 9, 19, 57, 50, 150, DateTimeKind.Local).AddTicks(2009), "https://www.webshop.com/success" });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AcquirerId", "AcquirerName", "Amount", "IssuerId", "IssuerName", "PaymentId", "Timestamp", "TransactionStatus" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341235"), "Acquirer name", 444.0, new Guid("12345678-1234-1234-1234-123412341234"), "Issuer name", new Guid("12345678-1234-1234-1234-123412341234"), new DateTime(2021, 12, 9, 19, 57, 50, 153, DateTimeKind.Local).AddTicks(3222), 1 });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Discriminator", "MerchantId", "MerchantPassword", "Name" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341235"), "Merchant", new Guid("12345678-1234-1234-1234-123412341234"), "password", "Merchant name" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "DateOfBirth", "Discriminator", "EmailAddress", "FirstName", "LastName", "UniquePersonalRegistrationNumber" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "RegisteredUser", "user@gmail.com", "FirstName", "LastName", "123456789" },
                    { new Guid("d969bb55-393a-4b22-9507-f4b492b3413f"), new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "RegisteredUser", "headOfAcquirement@gmail.com", "headOfAcquirement", "headOfAcquirement", "123456790" },
                    { new Guid("665166bf-411c-4ba9-a16d-2a6460a59500"), new DateTime(1990, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "RegisteredUser", "staff@gmail.com", "staff", "staff", "123456791" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "AccountNumber", "Balance", "UserId" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), "123456789", 100000.0, new Guid("12345678-1234-1234-1234-123412341235") },
                    { new Guid("12345678-1234-1234-1234-123412341235"), "222222222", 222222.0, new Guid("12345678-1234-1234-1234-123412341234") },
                    { new Guid("12345678-1234-1234-1234-123412341236"), "333333333", 222222.0, new Guid("d969bb55-393a-4b22-9507-f4b492b3413f") },
                    { new Guid("12345678-1234-1234-1234-123412341237"), "444444444", 222222.0, new Guid("665166bf-411c-4ba9-a16d-2a6460a59500") }
                });

            migrationBuilder.InsertData(
                table: "PaymentCards",
                columns: new[] { "Id", "CardOwnerId", "ExpirationDate", "HolderName", "PAN", "SecurityCode" },
                values: new object[,]
                {
                    { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("12345678-1234-1234-1234-123412341234"), "04/22", "Holder Name", "1234561234561234", "1234" },
                    { new Guid("12345678-1234-1234-1234-123412341235"), new Guid("d969bb55-393a-4b22-9507-f4b492b3413f"), "04/22", "Acquirer Name", "1234562222221234", "1234" },
                    { new Guid("12345678-1234-1234-1234-123412341236"), new Guid("665166bf-411c-4ba9-a16d-2a6460a59500"), "04/22", "Staff Name", "1234563333331234", "1234" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UserId",
                table: "Accounts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentCards_CardOwnerId",
                table: "PaymentCards",
                column: "CardOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PSPResponse_PSPRequestId",
                table: "PSPResponse",
                column: "PSPRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "PaymentCards");

            migrationBuilder.DropTable(
                name: "PSPResponse");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "PSPRequests");
        }
    }
}
