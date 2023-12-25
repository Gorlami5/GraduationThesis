using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationApp.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Companies_CompanyId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Reservations_ReservationId",
                table: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Photos_ReservationId",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "Photos");

            migrationBuilder.AddColumn<int>(
                name: "CompanyMainPhotoId",
                table: "Reservations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Photos",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_CompanyMainPhotoId",
                table: "Reservations",
                column: "CompanyMainPhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Companies_CompanyId",
                table: "Photos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Photos_CompanyMainPhotoId",
                table: "Reservations",
                column: "CompanyMainPhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_Companies_CompanyId",
                table: "Photos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Photos_CompanyMainPhotoId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_CompanyMainPhotoId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "CompanyMainPhotoId",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ReservationId",
                table: "Photos",
                column: "ReservationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Companies_CompanyId",
                table: "Photos",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_Reservations_ReservationId",
                table: "Photos",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
