using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedidsfromgamePlatofrmandgameGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2f37a868-c790-4db5-b16f-85c24c28ca87"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("382e7c5d-324b-41c2-ba7f-1c01cc5470c8"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3ceceaff-a1a9-44b9-9880-9c3fba5d7fb0"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("49ae79b8-96fb-47f7-92e2-14792a12dcad"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("49ed8216-98df-490c-b725-a257edeec6bf"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7af3d57d-a9f3-4b35-8d70-2c9b6a887fa0"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7fcbd360-65bc-4704-9cb1-f0f02e460465"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8b9e34ec-64e3-4133-8502-b4d845b55a81"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8d784d3e-3ec5-4357-951e-b85aabb7f557"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8f355ccd-5e7d-4668-a6e4-ce3eb22b91c8"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ea309af0-051d-4ab9-9a7f-9e2f87e170fb"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ed782957-6a7a-4119-8e72-924abafe8519"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("108bf3a5-da64-4d9f-9e8f-67b6c549f1b2"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("cc7ea164-502a-47df-a6fb-6e954c1d4392"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("ccc88a55-5c10-4f4e-987a-20a11f4ec9fc"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("dc807cf9-4bf6-4639-adbd-93db620a1dad"));

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GamePlatforms");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GameGenres");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("02e73b48-bf3e-45df-a107-c65f302ce8dc"), "RTS", new Guid("01e73b48-bf3e-45df-a107-c65f302ce8db") },
                    { new Guid("03e73b48-bf3e-45df-a107-c65f302ce8dd"), "TBS", new Guid("01e73b48-bf3e-45df-a107-c65f302ce8db") },
                    { new Guid("04e73b48-bf3e-45df-a107-c65f302ce8de"), "RPG", null },
                    { new Guid("05e73b48-bf3e-45df-a107-c65f302ce8df"), "Sports", null },
                    { new Guid("07e73b48-bf3e-45df-a107-c65f302ce8e1"), "Rally", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") },
                    { new Guid("08e73b48-bf3e-45df-a107-c65f302ce8e2"), "Arcade", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") },
                    { new Guid("09e73b48-bf3e-45df-a107-c65f302ce8e3"), "Formula", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") },
                    { new Guid("10e73b48-bf3e-45df-a107-c65f302ce8e4"), "Off-road", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") },
                    { new Guid("12e73b48-bf3e-45df-a107-c65f302ce8e6"), "FPS", new Guid("8ba799e7-6a4a-4302-b6cc-e5ab871cd8aa") },
                    { new Guid("13e73b48-bf3e-45df-a107-c65f302ce8e7"), "TPS", new Guid("8ba799e7-6a4a-4302-b6cc-e5ab871cd8aa") },
                    { new Guid("14e73b48-bf3e-45df-a107-c65f302ce8e8"), "Adventure", null },
                    { new Guid("15e73b48-bf3e-45df-a107-c65f302ce8e9"), "Puzzle & Skill", null }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("01f73b48-bf3e-45df-a107-c65f302ce8fa"), "Mobile" },
                    { new Guid("02f73b48-bf3e-45df-a107-c65f302ce8fb"), "Browser" },
                    { new Guid("03f73b48-bf3e-45df-a107-c65f302ce8fc"), "Desktop" },
                    { new Guid("04f73b48-bf3e-45df-a107-c65f302ce8fd"), "Console" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("02e73b48-bf3e-45df-a107-c65f302ce8dc"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("03e73b48-bf3e-45df-a107-c65f302ce8dd"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("04e73b48-bf3e-45df-a107-c65f302ce8de"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("05e73b48-bf3e-45df-a107-c65f302ce8df"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("07e73b48-bf3e-45df-a107-c65f302ce8e1"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("08e73b48-bf3e-45df-a107-c65f302ce8e2"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("09e73b48-bf3e-45df-a107-c65f302ce8e3"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("10e73b48-bf3e-45df-a107-c65f302ce8e4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("12e73b48-bf3e-45df-a107-c65f302ce8e6"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("13e73b48-bf3e-45df-a107-c65f302ce8e7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("14e73b48-bf3e-45df-a107-c65f302ce8e8"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("15e73b48-bf3e-45df-a107-c65f302ce8e9"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("01f73b48-bf3e-45df-a107-c65f302ce8fa"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("02f73b48-bf3e-45df-a107-c65f302ce8fb"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("03f73b48-bf3e-45df-a107-c65f302ce8fc"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("04f73b48-bf3e-45df-a107-c65f302ce8fd"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GamePlatforms",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GameGenres",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("2f37a868-c790-4db5-b16f-85c24c28ca87"), "RPG", null },
                    { new Guid("382e7c5d-324b-41c2-ba7f-1c01cc5470c8"), "FPS", new Guid("8ba799e7-6a4a-4302-b6cc-e5ab871cd8aa") },
                    { new Guid("3ceceaff-a1a9-44b9-9880-9c3fba5d7fb0"), "RTS", new Guid("01e73b48-bf3e-45df-a107-c65f302ce8db") },
                    { new Guid("49ae79b8-96fb-47f7-92e2-14792a12dcad"), "TBS", new Guid("01e73b48-bf3e-45df-a107-c65f302ce8db") },
                    { new Guid("49ed8216-98df-490c-b725-a257edeec6bf"), "Puzzle & Skill", null },
                    { new Guid("7af3d57d-a9f3-4b35-8d70-2c9b6a887fa0"), "Arcade", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") },
                    { new Guid("7fcbd360-65bc-4704-9cb1-f0f02e460465"), "Adventure", null },
                    { new Guid("8b9e34ec-64e3-4133-8502-b4d845b55a81"), "Rally", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") },
                    { new Guid("8d784d3e-3ec5-4357-951e-b85aabb7f557"), "Sports", null },
                    { new Guid("8f355ccd-5e7d-4668-a6e4-ce3eb22b91c8"), "TPS", new Guid("8ba799e7-6a4a-4302-b6cc-e5ab871cd8aa") },
                    { new Guid("ea309af0-051d-4ab9-9a7f-9e2f87e170fb"), "Formula", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") },
                    { new Guid("ed782957-6a7a-4119-8e72-924abafe8519"), "Off-road", new Guid("4ebd532a-2c0f-4b3a-96c3-06d77f7a432d") }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("108bf3a5-da64-4d9f-9e8f-67b6c549f1b2"), "Desktop" },
                    { new Guid("cc7ea164-502a-47df-a6fb-6e954c1d4392"), "Console" },
                    { new Guid("ccc88a55-5c10-4f4e-987a-20a11f4ec9fc"), "Browser" },
                    { new Guid("dc807cf9-4bf6-4639-adbd-93db620a1dad"), "Mobile" }
                });
        }
    }
}
