using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KampusRadar.Migrations
{
    /// <inheritdoc />
    public partial class InicijalnaMigracija : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dogadaji",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Naziv = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Opis = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    Kategorija = table.Column<int>(type: "INTEGER", nullable: false),
                    Pocetak = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Lokacija = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Cijena = table.Column<double>(type: "REAL", nullable: false),
                    BrojMjesta = table.Column<int>(type: "INTEGER", nullable: false),
                    PotrebnaPrijava = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogadaji", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dogadaji");
        }
    }
}
