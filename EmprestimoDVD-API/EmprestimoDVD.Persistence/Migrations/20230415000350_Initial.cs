using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace EmprestimoDVD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaixaEtaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    De = table.Column<int>(type: "int", nullable: false),
                    Ate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixaEtaria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumTelefone = table.Column<long>(type: "bigint", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaixaEtariaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoa_FaixaEtaria_FaixaEtariaId",
                        column: x => x.FaixaEtariaId,
                        principalTable: "FaixaEtaria",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DVD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sinopse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassificacaoId = table.Column<int>(type: "int", nullable: true),
                    ArtistaPrincipalId = table.Column<int>(type: "int", nullable: true),
                    DiretorId = table.Column<int>(type: "int", nullable: true),
                    GeneroId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DVD", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DVD_FaixaEtaria_ClassificacaoId",
                        column: x => x.ClassificacaoId,
                        principalTable: "FaixaEtaria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DVD_Genero_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Genero",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DVD_Pessoa_ArtistaPrincipalId",
                        column: x => x.ArtistaPrincipalId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DVD_Pessoa_DiretorId",
                        column: x => x.DiretorId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Emprestimo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataEmprestimo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDevolver = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmigoId = table.Column<int>(type: "int", nullable: true),
                    DVDId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emprestimo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emprestimo_DVD_DVDId",
                        column: x => x.DVDId,
                        principalTable: "DVD",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Emprestimo_Pessoa_AmigoId",
                        column: x => x.AmigoId,
                        principalTable: "Pessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DVD_ArtistaPrincipalId",
                table: "DVD",
                column: "ArtistaPrincipalId");

            migrationBuilder.CreateIndex(
                name: "IX_DVD_ClassificacaoId",
                table: "DVD",
                column: "ClassificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_DVD_DiretorId",
                table: "DVD",
                column: "DiretorId");

            migrationBuilder.CreateIndex(
                name: "IX_DVD_GeneroId",
                table: "DVD",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimo_AmigoId",
                table: "Emprestimo",
                column: "AmigoId");

            migrationBuilder.CreateIndex(
                name: "IX_Emprestimo_DVDId",
                table: "Emprestimo",
                column: "DVDId");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_FaixaEtariaId",
                table: "Pessoa",
                column: "FaixaEtariaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emprestimo");

            migrationBuilder.DropTable(
                name: "DVD");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Pessoa");

            migrationBuilder.DropTable(
                name: "FaixaEtaria");
        }
    }
}
