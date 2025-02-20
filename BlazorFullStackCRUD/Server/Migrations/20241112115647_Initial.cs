﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorFullStackCRUD.Server.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperVillains",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VillainName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ComicId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperHeroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperHeroes_Comics_ComicId",
                        column: x => x.ComicId,
                        principalTable: "Comics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Comics",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Marvel" });

            migrationBuilder.InsertData(
                table: "Comics",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "DC" });

            migrationBuilder.InsertData(
                table: "SuperVillains",
                columns: new[] { "Id", "ComicId", "FirstName", "LastName", "VillainName" },
                values: new object[] { 1, 1, "Victor", "Von Doom", "Dr.Doom" });

            migrationBuilder.InsertData(
                table: "SuperVillains",
                columns: new[] { "Id", "ComicId", "FirstName", "LastName", "VillainName" },
                values: new object[] { 2, 2, "Barry", "Allen", "Savitar" });

            migrationBuilder.CreateIndex(
                name: "IX_SuperHeroes_ComicId",
                table: "SuperVillains",
                column: "ComicId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperVillains");

            migrationBuilder.DropTable(
                name: "Comics");
        }
    }
}
