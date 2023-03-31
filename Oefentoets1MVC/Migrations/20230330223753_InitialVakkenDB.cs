using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oefentoets1MVC.Migrations
{
    public partial class InitialVakkenDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vakken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naam = table.Column<string>(type: "TEXT", nullable: false),
                    EC = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vakken", x => x.Id);
                    table.UniqueConstraint("AK_Vakken_Naam", x => x.Naam);
                });

            migrationBuilder.CreateTable(
                name: "Pogingen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Jaar = table.Column<int>(type: "INTEGER", nullable: false),
                    Resultaat = table.Column<int>(type: "INTEGER", nullable: false),
                    VakNaam = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pogingen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pogingen_Vakken_VakNaam",
                        column: x => x.VakNaam,
                        principalTable: "Vakken",
                        principalColumn: "Naam",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Vakken",
                columns: new[] { "Id", "EC", "Naam" },
                values: new object[] { 1, 4, "Server" });

            migrationBuilder.InsertData(
                table: "Vakken",
                columns: new[] { "Id", "EC", "Naam" },
                values: new object[] { 2, 4, "C#" });

            migrationBuilder.InsertData(
                table: "Vakken",
                columns: new[] { "Id", "EC", "Naam" },
                values: new object[] { 3, 3, "Databases" });

            migrationBuilder.InsertData(
                table: "Vakken",
                columns: new[] { "Id", "EC", "Naam" },
                values: new object[] { 4, 3, "UML" });

            migrationBuilder.InsertData(
                table: "Vakken",
                columns: new[] { "Id", "EC", "Naam" },
                values: new object[] { 5, 9, "KBS" });

            migrationBuilder.CreateIndex(
                name: "IX_Pogingen_VakNaam",
                table: "Pogingen",
                column: "VakNaam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pogingen");

            migrationBuilder.DropTable(
                name: "Vakken");
        }
    }
}
