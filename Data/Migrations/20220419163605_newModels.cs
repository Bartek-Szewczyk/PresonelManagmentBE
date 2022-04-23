using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PresonelManagmentBE.Migrations
{
    public partial class newModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "StaffUsers");

            migrationBuilder.AddColumn<byte>(
                name: "CategoryId",
                table: "StaffUsers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "HourlyRate",
                table: "StaffUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "tinyint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SumOfHours = table.Column<int>(type: "int", nullable: false),
                    SumOfReports = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReportHistories_StaffUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "StaffUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffUsers_CategoryId",
                table: "StaffUsers",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ReportHistories_UserId",
                table: "ReportHistories",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffUsers_Categories_CategoryId",
                table: "StaffUsers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffUsers_Categories_CategoryId",
                table: "StaffUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ReportHistories");

            migrationBuilder.DropIndex(
                name: "IX_StaffUsers_CategoryId",
                table: "StaffUsers");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "StaffUsers");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "StaffUsers");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "StaffUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
