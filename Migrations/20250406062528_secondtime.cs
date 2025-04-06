using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UITraining.Migrations
{
    /// <inheritdoc />
    public partial class secondtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 6, 6, 25, 27, 759, DateTimeKind.Utc).AddTicks(2444), "EffGMEXzSpnrgJEma0w2xJex9/c7gOoteRBOoioiI40c7Jwpi9ZWIcV1CwIXoTwBqNWMhh4Ebo2pw6SNz/n+ag==", "rLymIHox8ue8x4jJkejpQA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "PasswordHash", "Salt" },
                values: new object[] { new DateTime(2025, 4, 6, 5, 14, 4, 572, DateTimeKind.Utc).AddTicks(2987), "1gCprDxV7KFn1tZH+c/PjGm1B0vI1FRaYSJePuQjsdWdsa+ArOXWXtPkhWuUafwnZIGtASV1V5zsbMhThHdwbQ==", "EJ1QvfquUhhAGZH1dKJ3/A==" });
        }
    }
}
