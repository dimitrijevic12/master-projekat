using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebShop.DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ITRole = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accommodations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostPerNight = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accommodations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accommodations_Admins_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conferences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conferences_Admins_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Online = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Admins_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ProductKey = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ProductKey);
                    table.ForeignKey(
                        name: "FK_Items_Admins_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transportations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    StartDestination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalDestination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transportations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transportations_Admins_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalPrice = table.Column<double>(type: "float", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Admins_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Admins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_RegisteredUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "RegisteredUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransactionItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransactionItems_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "Id", "Email", "MerchantId", "Name", "Password", "Username" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "admin@gmail.com", new Guid("12345678-1234-1234-1234-123412341234"), "Admin", "password", "admin" });

            migrationBuilder.InsertData(
                table: "RegisteredUsers",
                columns: new[] { "Id", "Address", "Email", "FirstName", "ITRole", "LastName", "Password", "PhoneNumber", "PostalCode", "Username" },
                values: new object[,]
                {
                    { new Guid("318c2ae2-e63d-4128-b987-b451ee1af6fd"), "Novosadska 14", "issuer@gmail.com", "issuer", "default", "issuer", "password", "123456789", "1234", "issuer" },
                    { new Guid("a50f09e8-cdc5-4c0d-884d-c183f67a32bd"), "Beogradska 26", "headOfAcquirement@gmail.com", "headOfAcquirement", "headOfAcquirement", "headOfAcquirement", "password", "123456789", "1234", "headOfAcquirement" },
                    { new Guid("c88e7ad4-1866-4a3e-b07f-65d5e328a91b"), "Novosadska 14", "staff@gmail.com", "staff", "staff", "staff", "password", "123456789", "1234", "staff" }
                });

            migrationBuilder.InsertData(
                table: "Accommodations",
                columns: new[] { "Id", "Address", "City", "CostPerNight", "Description", "ImagePath", "Name", "OwnerId" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "Beogradska 14", "Belgrade", 1000.0, "AccomodationDesc", "95024620.jpg", "AccomodationName", new Guid("12345678-1234-1234-1234-123412341234") });

            migrationBuilder.InsertData(
                table: "Conferences",
                columns: new[] { "Id", "Address", "Date", "Description", "ImagePath", "Name", "Online", "OwnerId", "Price" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "Beogradska 14", new DateTime(2022, 1, 4, 14, 28, 5, 849, DateTimeKind.Local).AddTicks(5688), "ConferenceDesc", "conference12345123.jpg", "ConferenceName", false, new Guid("12345678-1234-1234-1234-123412341234"), 1400.0 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Address", "Description", "EndDate", "ImagePath", "Name", "Online", "OwnerId", "Price", "StartDate" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "Beogradska 14", "CourseDesc", new DateTime(2022, 1, 4, 14, 28, 5, 854, DateTimeKind.Local).AddTicks(8895), "conferences-integrated-systems-events-1500x630-2.jpg", "CourseName", false, new Guid("12345678-1234-1234-1234-123412341234"), 1400.0, new DateTime(2022, 1, 4, 14, 28, 5, 854, DateTimeKind.Local).AddTicks(8862) });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ProductKey", "Description", "ImagePath", "Name", "OwnerId", "Price" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "ItemDesc", "bed.jpg", "ItemName", new Guid("12345678-1234-1234-1234-123412341234"), 123.0 });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "BuyerId", "Currency", "SellerId", "Status", "Timestamp", "TotalPrice" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), new Guid("318c2ae2-e63d-4128-b987-b451ee1af6fd"), "EUR", new Guid("12345678-1234-1234-1234-123412341234"), 0, new DateTime(2022, 1, 4, 14, 28, 5, 855, DateTimeKind.Local).AddTicks(3904), 1640.0 });

            migrationBuilder.InsertData(
                table: "Transportations",
                columns: new[] { "Id", "DepartureTime", "Description", "FinalDestination", "ImagePath", "Name", "OwnerId", "Price", "StartDestination" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), new DateTime(2022, 1, 4, 14, 28, 5, 856, DateTimeKind.Local).AddTicks(2250), "TransportationDesc", "Novi Sad", "conferences-integrated-systems-events-1500x630-2.jpg", "TransportationName", new Guid("12345678-1234-1234-1234-123412341234"), 1000.0, "Belgrade" });

            migrationBuilder.InsertData(
                table: "TransactionItems",
                columns: new[] { "Id", "Name", "Price", "ProductId", "Quantity", "TransactionId", "Type" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341234"), "ItemName", 150.0, new Guid("12345678-1234-1234-1234-123412341234"), 4, new Guid("12345678-1234-1234-1234-123412341234"), 4 });

            migrationBuilder.InsertData(
                table: "TransactionItems",
                columns: new[] { "Id", "Name", "Price", "ProductId", "Quantity", "TransactionId", "Type" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123412341235"), "CourseName", 1400.0, new Guid("12345678-1234-1234-1234-123412341234"), 1, new Guid("12345678-1234-1234-1234-123412341234"), 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_OwnerId",
                table: "Accommodations",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Conferences_OwnerId",
                table: "Conferences",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_OwnerId",
                table: "Courses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BuyerId",
                table: "Transactions",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SellerId",
                table: "Transactions",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transportations_OwnerId",
                table: "Transportations",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accommodations");

            migrationBuilder.DropTable(
                name: "Conferences");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "TransactionItems");

            migrationBuilder.DropTable(
                name: "Transportations");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "RegisteredUsers");
        }
    }
}
