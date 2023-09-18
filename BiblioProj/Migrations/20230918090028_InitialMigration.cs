using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BiblioProj.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Auteur",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Prenom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auteur", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnneeDePublication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuteurId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livre_Auteur_AuteurId",
                        column: x => x.AuteurId,
                        principalTable: "Auteur",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pret",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Emprunteur = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateDeRetour = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LivreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pret", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pret_Livre_LivreId",
                        column: x => x.LivreId,
                        principalTable: "Livre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livre_AuteurId",
                table: "Livre",
                column: "AuteurId");

            migrationBuilder.CreateIndex(
                name: "IX_Pret_LivreId",
                table: "Pret",
                column: "LivreId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pret");

            migrationBuilder.DropTable(
                name: "Livre");

            migrationBuilder.DropTable(
                name: "Auteur");
        }
    }
}
