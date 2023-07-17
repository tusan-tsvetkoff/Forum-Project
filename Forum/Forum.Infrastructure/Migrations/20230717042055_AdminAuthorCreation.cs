using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdminAuthorCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("428d6f32-4c58-46c1-a3ab-0cb05b06a034"));

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "AvatarUrl", "CreatedDateTime", "FirstName", "LastName", "UserId", "Username" },
                values: new object[] { "Author_bc6f4adc-1a0a-4870-b495-a0d7b6b1aac2", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "User", new Guid("bc6f4adc-1a0a-4870-b495-a0d7b6b1aac2"), "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsAdmin", "LastName", "Password", "PhoneNumber", "Username" },
                values: new object[] { new Guid("bc6f4adc-1a0a-4870-b495-a0d7b6b1aac2"), new DateTime(2023, 7, 17, 4, 20, 55, 392, DateTimeKind.Utc).AddTicks(4520), "admin4etotochkakom@example.com", "Admin", true, "User", "adminskaparola", null, "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: "Author_bc6f4adc-1a0a-4870-b495-a0d7b6b1aac2");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bc6f4adc-1a0a-4870-b495-a0d7b6b1aac2"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsAdmin", "LastName", "Password", "PhoneNumber", "Username" },
                values: new object[] { new Guid("428d6f32-4c58-46c1-a3ab-0cb05b06a034"), new DateTime(2023, 7, 17, 4, 16, 38, 932, DateTimeKind.Utc).AddTicks(1289), "admin4etotochkakom@example.com", "Admin", true, "User", "adminskaparola", null, "admin" });
        }
    }
}
