using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oefentoets1MVC.Migrations
{
    public partial class StudentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentType",
                table: "Pogingen",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentType",
                table: "Pogingen");
        }
    }
}
