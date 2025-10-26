using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LojaCamisasGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CamisasGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    NomeTime = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Jogo = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Tamanho = table.Column<string>(type: "TEXT", maxLength: 10, nullable: false),
                    Cor = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QuantidadeEstoque = table.Column<int>(type: "INTEGER", nullable: false),
                    Disponivel = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: true),
                    DataCadastro = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CamisasGames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CamisasGames",
                columns: new[] { "Id", "Cor", "DataCadastro", "Disponivel", "Jogo", "Nome", "NomeTime", "Preco", "QuantidadeEstoque", "Tamanho" },
                values: new object[,]
                {
                    { 1, "Azul e Branco", new DateTime(2025, 10, 26, 23, 41, 33, 440, DateTimeKind.Local).AddTicks(2898), true, "League of Legends", "Camisa Oficial Team Liquid 2024", "Team Liquid", 149.90m, 50, "M" },
                    { 2, "Preto e Rosa", new DateTime(2025, 10, 26, 23, 41, 33, 440, DateTimeKind.Local).AddTicks(2903), true, "CS:GO", "Camisa FURIA Esports Home", "FURIA", 159.90m, 30, "G" },
                    { 3, "Verde e Branco", new DateTime(2025, 10, 26, 23, 41, 33, 440, DateTimeKind.Local).AddTicks(2907), true, "Valorant", "Camisa LOUD Champions", "LOUD", 169.90m, 40, "M" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CamisasGames");
        }
    }
}
