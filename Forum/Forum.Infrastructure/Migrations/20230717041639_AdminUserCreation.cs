using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminUserCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsAdmin", "LastName", "Password", "PhoneNumber", "Username" },
                values: new object[] { new Guid("428d6f32-4c58-46c1-a3ab-0cb05b06a034"), new DateTime(2023, 7, 17, 4, 16, 38, 932, DateTimeKind.Utc).AddTicks(1289), "admin4etotochkakom@example.com", "Admin", true, "User", "adminskaparola", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("428d6f32-4c58-46c1-a3ab-0cb05b06a034"));
        }
    }
}
