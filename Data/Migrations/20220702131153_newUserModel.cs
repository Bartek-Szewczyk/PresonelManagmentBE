using Microsoft.EntityFrameworkCore.Migrations;

namespace PresonelManagmentBE.Migrations
{
    public partial class newUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Categories_CategoryId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<byte>(
                name: "CategoryId",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HourlyRate",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Categories_CategoryId",
                table: "AspNetUsers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Categories_CategoryId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HourlyRate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<byte>(
                name: "CategoryId",
                table: "AspNetUsers",
                type: "tinyint",
                nullable: true,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Categories_CategoryId",
                table: "AspNetUsers",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
