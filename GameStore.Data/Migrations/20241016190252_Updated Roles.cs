using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Resources_ResourceId",
                table: "Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions");

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("0019bfaa-6339-4747-bfdd-f24ffd37e7e9"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("07b54cfc-a56e-47a4-b18f-d3a1be398f2e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("131cab9a-eef6-4836-85e1-86102abf9063"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("20449ac2-d11d-438e-be04-b9c1babf424f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2ca38656-0ee8-4973-aacc-22f847e3abae"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2d9a6e53-3240-415a-b49c-8e5f32d83c47"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("2f256bf1-18f4-40c5-8c72-18d351d2e535"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("34080a89-1998-49d0-84e7-8891d7309b6c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4767f2f8-f92c-4661-9a78-95b0b6754f4d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("4a2b1a0f-9921-4015-b6ce-d27a7cd7a087"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("5123946b-4666-40e4-9eae-961527ce0a73"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("55d7ba56-f132-49ab-b282-64b4ccf7e3fe"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("56c96b36-4313-4039-8155-6a48135cd1a6"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("65145a7d-e602-4708-99a8-09168537e01b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("69f4a664-abb4-4c8d-b103-9207c1841482"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("6ea8e687-03a1-42f1-96a4-160407b8028a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("7d7edb67-95af-40f5-b5c2-0bd1f67ee371"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("88601979-45c1-4b4f-98f5-fe0da0ee8e93"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8abaeedc-5eb8-449c-95bb-504ffef31ad3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("8b4ee723-8f5c-4594-89b8-29daa4f52471"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("9d276ad0-cc81-421f-a4ee-e5718233a665"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a3688480-8a8f-44da-a30f-16ecae13a598"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("a63c2127-a9aa-4904-b445-c4d12b5cebae"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("aa08c59c-9db5-4cf5-a3c2-07969b698b3a"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ae506a43-38f3-4e37-85dc-a94ddf8e5c4d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c480ff7a-0ae6-422d-960f-0774dcc04cd3"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("c7a08f02-8d47-4201-af88-f6647527171d"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("cf664bfe-6f5f-43d9-9cce-4ac2b5247c0c"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e5b8e26d-0b3d-4d00-bad3-fec98da6cb68"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("e8eacfde-7882-4e9b-8a71-61af3c609e8e"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("eafe705c-c3d9-4717-a71f-47ac455a385f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f088f367-445f-4174-973e-6485c8873f9b"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("f65355cd-56a2-486c-84f3-09373e68d56f"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("ff6b24dc-c5d6-4d5d-8cb0-e5fd76d9ec88"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "Permissions");

            migrationBuilder.AddColumn<int>(
                name: "Resource",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AccessType", "Resource", "RoleId" },
                values: new object[,]
                {
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3040"), 1, 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3041"), 1, 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3042"), 1, 2, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3043"), 1, 3, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3044"), 1, 4, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3045"), 1, 5, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3046"), 1, 6, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3050"), 1, 0, new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3051"), 1, 1, new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3052"), 1, 3, new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3053"), 1, 4, new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3054"), 1, 5, new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3055"), 0, 6, new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3060"), 1, 0, new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3061"), 0, 1, new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3062"), 0, 1, new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3063"), 0, 3, new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3064"), 0, 4, new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3065"), 0, 5, new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3066"), 0, 6, new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3070"), 0, 0, new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3071"), 0, 1, new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3072"), 0, 2, new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3073"), 0, 3, new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3074"), 0, 4, new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3075"), 0, 5, new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3076"), 0, 6, new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3080"), 0, 0, new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3081"), 0, 1, new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3082"), 0, 2, new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3083"), 0, 3, new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3084"), 0, 4, new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3085"), 0, 5, new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3086"), 0, 6, new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3040"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3041"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3042"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3043"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3044"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3045"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3046"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3050"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3051"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3052"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3053"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3054"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3055"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3060"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3061"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3062"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3063"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3064"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3065"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3066"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3070"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3071"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3072"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3073"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3074"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3075"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3076"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3080"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3081"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3082"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3083"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3084"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3085"));

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3086"));

            migrationBuilder.DropColumn(
                name: "Resource",
                table: "Permissions");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ResourceId",
                table: "Permissions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Resources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Resources",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3031"), "Games" },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3032"), "Orders" },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3033"), "Comments" },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3034"), "Genres" },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3035"), "Platforms" },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3036"), "Publishers" },
                    { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3037"), "Users" }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5"),
                column: "UserId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030"),
                column: "UserId",
                value: null);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "AccessType", "ResourceId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0019bfaa-6339-4747-bfdd-f24ffd37e7e9"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3033"), new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("07b54cfc-a56e-47a4-b18f-d3a1be398f2e"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3034"), new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("131cab9a-eef6-4836-85e1-86102abf9063"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3031"), new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("20449ac2-d11d-438e-be04-b9c1babf424f"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3032"), new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("2ca38656-0ee8-4973-aacc-22f847e3abae"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3032"), new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("2d9a6e53-3240-415a-b49c-8e5f32d83c47"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3037"), new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("2f256bf1-18f4-40c5-8c72-18d351d2e535"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3035"), new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("34080a89-1998-49d0-84e7-8891d7309b6c"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3037"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("4767f2f8-f92c-4661-9a78-95b0b6754f4d"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3035"), new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("4a2b1a0f-9921-4015-b6ce-d27a7cd7a087"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3034"), new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("5123946b-4666-40e4-9eae-961527ce0a73"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3033"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("55d7ba56-f132-49ab-b282-64b4ccf7e3fe"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3033"), new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("56c96b36-4313-4039-8155-6a48135cd1a6"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3032"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("65145a7d-e602-4708-99a8-09168537e01b"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3036"), new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("69f4a664-abb4-4c8d-b103-9207c1841482"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3036"), new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("6ea8e687-03a1-42f1-96a4-160407b8028a"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3033"), new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("7d7edb67-95af-40f5-b5c2-0bd1f67ee371"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3034"), new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("88601979-45c1-4b4f-98f5-fe0da0ee8e93"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3031"), new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("8abaeedc-5eb8-449c-95bb-504ffef31ad3"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3036"), new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("8b4ee723-8f5c-4594-89b8-29daa4f52471"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3032"), new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("9d276ad0-cc81-421f-a4ee-e5718233a665"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3035"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("a3688480-8a8f-44da-a30f-16ecae13a598"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3037"), new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("a63c2127-a9aa-4904-b445-c4d12b5cebae"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3031"), new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("aa08c59c-9db5-4cf5-a3c2-07969b698b3a"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3037"), new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("ae506a43-38f3-4e37-85dc-a94ddf8e5c4d"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3034"), new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee") },
                    { new Guid("c480ff7a-0ae6-422d-960f-0774dcc04cd3"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3034"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("c7a08f02-8d47-4201-af88-f6647527171d"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3035"), new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("cf664bfe-6f5f-43d9-9cce-4ac2b5247c0c"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3032"), new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0") },
                    { new Guid("e5b8e26d-0b3d-4d00-bad3-fec98da6cb68"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3035"), new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5") },
                    { new Guid("e8eacfde-7882-4e9b-8a71-61af3c609e8e"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3037"), new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("eafe705c-c3d9-4717-a71f-47ac455a385f"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3036"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("f088f367-445f-4174-973e-6485c8873f9b"), 1, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3031"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030") },
                    { new Guid("f65355cd-56a2-486c-84f3-09373e68d56f"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3031"), new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") },
                    { new Guid("ff6b24dc-c5d6-4d5d-8cb0-e5fd76d9ec88"), 0, new Guid("d27fc764-f53d-40cb-a6f9-629f433c3036"), new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions",
                column: "ResourceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Resources_ResourceId",
                table: "Permissions",
                column: "ResourceId",
                principalTable: "Resources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
