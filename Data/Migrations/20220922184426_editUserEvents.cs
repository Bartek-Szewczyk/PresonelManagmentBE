using Microsoft.EntityFrameworkCore.Migrations;

namespace PresonelManagmentBE.Migrations
{
    public partial class editUserEvents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "UserEvents",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "UserEvents");
        }
    }
}
