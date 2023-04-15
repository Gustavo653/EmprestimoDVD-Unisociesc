using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimoDVD.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DropFaixaEtaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DVD_FaixaEtaria_ClassificacaoId",
                table: "DVD");

            migrationBuilder.DropForeignKey(
                name: "FK_Pessoa_FaixaEtaria_FaixaEtariaId",
                table: "Pessoa");

            migrationBuilder.DropTable(
                name: "FaixaEtaria");

            migrationBuilder.DropIndex(
                name: "IX_Pessoa_FaixaEtariaId",
                table: "Pessoa");

            migrationBuilder.DropIndex(
                name: "IX_DVD_ClassificacaoId",
                table: "DVD");

            migrationBuilder.DropColumn(
                name: "FaixaEtariaId",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "ClassificacaoId",
                table: "DVD");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Pessoa",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdadeMinima",
                table: "DVD",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Pessoa");

            migrationBuilder.DropColumn(
                name: "IdadeMinima",
                table: "DVD");

            migrationBuilder.AddColumn<int>(
                name: "FaixaEtariaId",
                table: "Pessoa",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClassificacaoId",
                table: "DVD",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FaixaEtaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ate = table.Column<int>(type: "int", nullable: false),
                    De = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixaEtaria", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pessoa_FaixaEtariaId",
                table: "Pessoa",
                column: "FaixaEtariaId");

            migrationBuilder.CreateIndex(
                name: "IX_DVD_ClassificacaoId",
                table: "DVD",
                column: "ClassificacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DVD_FaixaEtaria_ClassificacaoId",
                table: "DVD",
                column: "ClassificacaoId",
                principalTable: "FaixaEtaria",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoa_FaixaEtaria_FaixaEtariaId",
                table: "Pessoa",
                column: "FaixaEtariaId",
                principalTable: "FaixaEtaria",
                principalColumn: "Id");
        }
    }
}
