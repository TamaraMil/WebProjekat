using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankaProjekat.Migrations
{
    public partial class verzija1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    JMBG = table.Column<long>(type: "bigint", nullable: false),
                    DatumRodjenja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transakcije",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumTransakcije = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Suma = table.Column<double>(type: "float", nullable: false),
                    Valuta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transakcije", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VrsteRacuna",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vrstaNaziv = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VrsteRacunaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VrsteRacuna", x => x.id);
                    table.ForeignKey(
                        name: "FK_VrsteRacuna_VrsteRacuna_VrsteRacunaid",
                        column: x => x.VrsteRacunaid,
                        principalTable: "VrsteRacuna",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Nalog",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Lozinka = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    admin = table.Column<bool>(type: "bit", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nalog", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Nalog_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Racun",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Broj = table.Column<long>(type: "bigint", nullable: false),
                    Stanje = table.Column<double>(type: "float", nullable: false),
                    Valuta = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DatumOtvaranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DatumZatvaranja = table.Column<DateTime>(type: "datetime2", nullable: false),
                    korisnikID = table.Column<int>(type: "int", nullable: true),
                    vrstaRacunaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racun", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Racun_Korisnik_korisnikID",
                        column: x => x.korisnikID,
                        principalTable: "Korisnik",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Racun_VrsteRacuna_vrstaRacunaid",
                        column: x => x.vrstaRacunaid,
                        principalTable: "VrsteRacuna",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_Nalog_korisnikID",
                table: "Nalog",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_korisnikID",
                table: "Racun",
                column: "korisnikID");

            migrationBuilder.CreateIndex(
                name: "IX_Racun_vrstaRacunaid",
                table: "Racun",
                column: "vrstaRacunaid");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_RacunID",
                table: "Spoj",
                column: "RacunID");

            migrationBuilder.CreateIndex(
                name: "IX_Spoj_TransakcijeID",
                table: "Spoj",
                column: "TransakcijeID");

            migrationBuilder.CreateIndex(
                name: "IX_VrsteRacuna_VrsteRacunaid",
                table: "VrsteRacuna",
                column: "VrsteRacunaid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Nalog");

            migrationBuilder.DropTable(
                name: "Spoj");

            migrationBuilder.DropTable(
                name: "Racun");

            migrationBuilder.DropTable(
                name: "Transakcije");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "VrsteRacuna");
        }
    }
}
