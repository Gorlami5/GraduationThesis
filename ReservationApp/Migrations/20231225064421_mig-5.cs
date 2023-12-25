using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ReservationApp.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Companies_CompanyId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_CompanyId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Photos");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Photos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "CompanyPhotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DateAdded = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsMain = table.Column<bool>(type: "boolean", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPhotos_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CityId",
                table: "Companies",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPhotos_CompanyId",
                table: "CompanyPhotos",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_Cities_CityId",
                table: "Companies",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_Cities_CityId",
                table: "Companies");

            migrationBuilder.DropTable(
                name: "CompanyPhotos");

            migrationBuilder.DropIndex(
                name: "IX_Companies_CityId",
                table: "Companies");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Photos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Photos",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_CompanyId",
                table: "Photos",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Companies_CompanyId",
                table: "Photos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
