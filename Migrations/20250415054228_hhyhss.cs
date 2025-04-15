using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UITraining.Migrations
{
    /// <inheritdoc />
    public partial class hhyhss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Product2s",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 15, 5, 42, 28, 228, DateTimeKind.Utc).AddTicks(6169), "aim8QgzIW6wfJl7pcH0CUqIN3YeV8eKQpIpDb+xHPDN3MUiLwU87EtZTkHfRaXIWqd5+DoptiQnFMaDaXwdVbQ==", "ZH1WgISQk8Dkmjr+/ozIfw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Product2s");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 15, 5, 17, 26, 809, DateTimeKind.Utc).AddTicks(6096), "B7fWXbEbQIms1vWpffVOD+2sLX46ENvDafgclDwcFM8L28C2APGb7FjPcYEegn7iJYO0mMt5YdzpqGN93qir8Q==", "bw7uSWCzG+6NcMPakua+YQ==" });
        }
    }
}
