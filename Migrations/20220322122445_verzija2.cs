using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankaProjekat.Migrations
{
    public partial class verzija2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Spoj");

            migrationBuilder.DropTable(
                name: "Transakcije");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transakcije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumTransakcije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Suma = table.Column<double>(type: "float", nullable: false),
                    Valuta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Spoj",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RacunID = table.Column<int>(type: "int", nullable: true),
                    TransakcijeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spoj", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Spoj_Racun_RacunID",
                        column: x => x.RacunID,
                        principalTable: "Racun",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Spoj_Transakcije_TransakcijeID",
                        column: x => x.TransakcijeID,
                        principalTable: "Transakcije",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_RacunID",
                table: "Spoj",
                column: "RacunID");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_TransakcijeID",
                table: "Spoj",
                column: "TransakcijeID");
        }
    }
}
