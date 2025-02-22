using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedGenreEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OriginalId",
                table: "Genres",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("01e73b48-bf3e-45df-a107-c65f302ce8db"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("02e73b48-bf3e-45df-a107-c65f302ce8dc"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("03e73b48-bf3e-45df-a107-c65f302ce8dd"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("04e73b48-bf3e-45df-a107-c65f302ce8de"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("05e73b48-bf3e-45df-a107-c65f302ce8df"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("07e73b48-bf3e-45df-a107-c65f302ce8e1"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("08e73b48-bf3e-45df-a107-c65f302ce8e2"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("09e73b48-bf3e-45df-a107-c65f302ce8e3"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("10e73b48-bf3e-45df-a107-c65f302ce8e4"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("12e73b48-bf3e-45df-a107-c65f302ce8e6"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("13e73b48-bf3e-45df-a107-c65f302ce8e7"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("14e73b48-bf3e-45df-a107-c65f302ce8e8"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("15e73b48-bf3e-45df-a107-c65f302ce8e9"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d"),
                column: "OriginalId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8ba799e7-6a4a-4302-b6cc-e5ab871cd8aa"),
                column: "OriginalId",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalId",
                table: "Genres");
        }
    }
}
