using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updateddataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Name", "Password" },
                values: new object[] { new Guid("d27fc764-f53d-40cb-a6f9-629f433c4040"), "admin", "Administrator", "$2a$11$rnDA3Wpnf85Ti4m.U6PgO.toBvinBAmNG8nvVkbUWeCAQggcDbAXe" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId", "Id" },
                values: new object[] { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c4040"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c5050") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("d27fc764-f53d-40cb-a6f9-629f433c3030"), new Guid("d27fc764-f53d-40cb-a6f9-629f433c4040") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d27fc764-f53d-40cb-a6f9-629f433c4040"));
        }
    }
}
