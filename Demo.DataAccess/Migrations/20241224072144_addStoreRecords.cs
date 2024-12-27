using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Demo.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addStoreRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "stores",
                columns: new[] { "Id", "Address", "City", "Description", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "台中市北區三民路123號", "台中市", "鄰近一中商圈", "台中一中店", "0987654321" },
                    { 2, "台北市大安區大安路123號", "台北市", "熱鬧台北商圈", "台北大安店", "0911111111" },
                    { 3, "台南市安平區安平路123號", "台南市", "文化台南商圈", "台南安平店", "0922222222" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "stores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "stores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "stores",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
