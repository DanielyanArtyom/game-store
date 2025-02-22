using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updatednewdbsets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannedUsers_User_UserId",
                table: "BannedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Role_RolesId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_User_UsersId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_UsersId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "Role");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserRoles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "UserRoles",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_User_Login",
                table: "Users",
                newName: "IX_Users_Login");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Name",
                table: "Roles",
                newName: "IX_Roles_Name");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

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

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessType = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ResourceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_Resources_ResourceId",
                        column: x => x.ResourceId,
                        principalTable: "Resources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permissions_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                table: "Roles",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[] { new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee"), "Guest", null });

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
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserId",
                table: "Roles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ResourceId",
                table: "Permissions",
                column: "ResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_RoleId",
                table: "Permissions",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_BannedUsers_Users_UserId",
                table: "BannedUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BannedUsers_Users_UserId",
                table: "BannedUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UserId",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Users_UserId",
                table: "UserRoles");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Resources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserId",
                table: "Roles");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("155f3369-d8da-4b69-8e90-72e8ac7022ee"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserRoles",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "UserRoles",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Login",
                table: "User",
                newName: "IX_User_Login");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Name",
                table: "Role",
                newName: "IX_Role_Name");

            migrationBuilder.AddColumn<int>(
                name: "Permissions",
                table: "Role",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                columns: new[] { "RolesId", "UsersId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("155f3369-d8da-4b69-8e90-72e8ac6021ce"),
                column: "Permissions",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("68768561-0ed1-4cfc-a43b-7c74e635dec0"),
                column: "Permissions",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("aad1fa8c-b73f-40a4-8a3d-84130cf352c5"),
                column: "Permissions",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030"),
                column: "Permissions",
                value: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UsersId",
                table: "UserRoles",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_BannedUsers_User_UserId",
                table: "BannedUsers",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Role_RolesId",
                table: "UserRoles",
                column: "RolesId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_User_UsersId",
                table: "UserRoles",
                column: "UsersId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
