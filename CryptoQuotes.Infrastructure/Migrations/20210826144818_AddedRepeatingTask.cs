using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CryptoQuotes.Infrastructure.Migrations
{
    public partial class AddedRepeatingTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoQuote_Cryptocurrency_CryptocurrencyId",
                table: "CryptoQuote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CryptoQuote",
                table: "CryptoQuote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cryptocurrency",
                table: "Cryptocurrency");

            migrationBuilder.RenameTable(
                name: "CryptoQuote",
                newName: "CryptoQuotes");

            migrationBuilder.RenameTable(
                name: "Cryptocurrency",
                newName: "Cryptocurrencies");

            migrationBuilder.RenameIndex(
                name: "IX_CryptoQuote_CryptocurrencyId",
                table: "CryptoQuotes",
                newName: "IX_CryptoQuotes_CryptocurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CryptoQuotes",
                table: "CryptoQuotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cryptocurrencies",
                table: "Cryptocurrencies",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "RepeatingTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    ExecuteDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepeatingTasks", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoQuotes_Cryptocurrencies_CryptocurrencyId",
                table: "CryptoQuotes",
                column: "CryptocurrencyId",
                principalTable: "Cryptocurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CryptoQuotes_Cryptocurrencies_CryptocurrencyId",
                table: "CryptoQuotes");

            migrationBuilder.DropTable(
                name: "RepeatingTasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CryptoQuotes",
                table: "CryptoQuotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cryptocurrencies",
                table: "Cryptocurrencies");

            migrationBuilder.RenameTable(
                name: "CryptoQuotes",
                newName: "CryptoQuote");

            migrationBuilder.RenameTable(
                name: "Cryptocurrencies",
                newName: "Cryptocurrency");

            migrationBuilder.RenameIndex(
                name: "IX_CryptoQuotes_CryptocurrencyId",
                table: "CryptoQuote",
                newName: "IX_CryptoQuote_CryptocurrencyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CryptoQuote",
                table: "CryptoQuote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cryptocurrency",
                table: "Cryptocurrency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CryptoQuote_Cryptocurrency_CryptocurrencyId",
                table: "CryptoQuote",
                column: "CryptocurrencyId",
                principalTable: "Cryptocurrency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
