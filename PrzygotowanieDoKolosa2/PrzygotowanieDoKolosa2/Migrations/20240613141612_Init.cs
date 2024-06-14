using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PrzygotowanieDoKolosa2.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoatStandard",
                columns: table => new
                {
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoatStandard", x => x.IdBoatStandard);
                });

            migrationBuilder.CreateTable(
                name: "ClientCategory",
                columns: table => new
                {
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountPerc = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCategory", x => x.IdClientCategory);
                });

            migrationBuilder.CreateTable(
                name: "Sailboat",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float(2)", precision: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailboat", x => x.IdSailboat);
                    table.ForeignKey(
                        name: "FK_Sailboat_BoatStandard_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandard",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pesel = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IdClientCategory = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                    table.ForeignKey(
                        name: "FK_Client_ClientCategory_IdClientCategory",
                        column: x => x.IdClientCategory,
                        principalTable: "ClientCategory",
                        principalColumn: "IdClientCategory",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    DateFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdBoatStandard = table.Column<int>(type: "int", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    NumOfBoats = table.Column<int>(type: "int", nullable: false),
                    Fulfilled = table.Column<byte>(type: "tinyint", nullable: false),
                    Price = table.Column<double>(type: "float(2)", precision: 2, nullable: true),
                    CancelReason = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservation_BoatStandard_IdBoatStandard",
                        column: x => x.IdBoatStandard,
                        principalTable: "BoatStandard",
                        principalColumn: "IdBoatStandard",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Client_IdClient",
                        column: x => x.IdClient,
                        principalTable: "Client",
                        principalColumn: "IdClient",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sailboat_Reservation",
                columns: table => new
                {
                    IdSailboat = table.Column<int>(type: "int", nullable: false),
                    IdReservation = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sailboat_Reservation", x => new { x.IdReservation, x.IdSailboat });
                    table.ForeignKey(
                        name: "FK_Sailboat_Reservation_Reservation_IdReservation",
                        column: x => x.IdReservation,
                        principalTable: "Reservation",
                        principalColumn: "IdReservation");
                    table.ForeignKey(
                        name: "FK_Sailboat_Reservation_Sailboat_IdSailboat",
                        column: x => x.IdSailboat,
                        principalTable: "Sailboat",
                        principalColumn: "IdSailboat");
                });

            migrationBuilder.InsertData(
                table: "BoatStandard",
                columns: new[] { "IdBoatStandard", "Level", "Name" },
                values: new object[,]
                {
                    { 1, 3, "Luxury" },
                    { 2, 1, "Melina" }
                });

            migrationBuilder.InsertData(
                table: "ClientCategory",
                columns: new[] { "IdClientCategory", "DiscountPerc", "Name" },
                values: new object[,]
                {
                    { 1, 30, "Vip" },
                    { 2, 0, "Standard" },
                    { 3, 50, "Svip" }
                });

            migrationBuilder.InsertData(
                table: "Client",
                columns: new[] { "IdClient", "Birthday", "Email", "IdClientCategory", "LastName", "Name", "Pesel" },
                values: new object[,]
                {
                    { 1, new DateTime(1999, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juan@gmail.com", 2, "Jun", "Juan", "23578629532" },
                    { 2, new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "PalmiXXX@onet.com", 3, "Palmowski", "Palmosz", "97679654" }
                });

            migrationBuilder.InsertData(
                table: "Sailboat",
                columns: new[] { "IdSailboat", "Capacity", "Description", "IdBoatStandard", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 10, "staroc i zlom", 2, "Milenials 1980", 200.0 },
                    { 2, 4, "nowiutkie jak glowa bobasa", 1, "Golec Ship", 2000.0 }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "IdReservation", "CancelReason", "Capacity", "DateFrom", "DateTo", "Fulfilled", "IdBoatStandard", "IdClient", "NumOfBoats", "Price" },
                values: new object[,]
                {
                    { 1, null, 5, new DateTime(2024, 6, 13, 16, 16, 11, 541, DateTimeKind.Local).AddTicks(5201), new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0, 2, 1, 2, null },
                    { 2, null, 2, new DateTime(2024, 6, 13, 16, 16, 11, 544, DateTimeKind.Local).AddTicks(5937), new DateTime(2024, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), (byte)0, 2, 1, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Sailboat_Reservation",
                columns: new[] { "IdReservation", "IdSailboat" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Client_IdClientCategory",
                table: "Client",
                column: "IdClientCategory");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdBoatStandard",
                table: "Reservation",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdClient",
                table: "Reservation",
                column: "IdClient");

            migrationBuilder.CreateIndex(
                name: "IX_Sailboat_IdBoatStandard",
                table: "Sailboat",
                column: "IdBoatStandard");

            migrationBuilder.CreateIndex(
                name: "IX_Sailboat_Reservation_IdSailboat",
                table: "Sailboat_Reservation",
                column: "IdSailboat");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sailboat_Reservation");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Sailboat");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "BoatStandard");

            migrationBuilder.DropTable(
                name: "ClientCategory");
        }
    }
}
