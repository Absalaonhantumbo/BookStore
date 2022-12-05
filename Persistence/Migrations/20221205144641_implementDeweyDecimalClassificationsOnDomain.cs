using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Persistence.Migrations
{
    public partial class implementDeweyDecimalClassificationsOnDomain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeweyDecimalClassificationId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Edition",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Page",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DeweyDecimalClassifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeweyDecimalClassifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_DeweyDecimalClassificationId",
                table: "Books",
                column: "DeweyDecimalClassificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SupplierId",
                table: "Books",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DeweyDecimalClassificatio~",
                table: "Books",
                column: "DeweyDecimalClassificationId",
                principalTable: "DeweyDecimalClassifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Suppliers_SupplierId",
                table: "Books",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_DeweyDecimalClassifications_DeweyDecimalClassificatio~",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Suppliers_SupplierId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "DeweyDecimalClassifications");

            migrationBuilder.DropIndex(
                name: "IX_Books_DeweyDecimalClassificationId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_SupplierId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DeweyDecimalClassificationId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Edition",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Page",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Books");
        }
    }
}
