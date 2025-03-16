using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UITraining.Migrations
{
    /// <inheritdoc />
    public partial class deletestatusduaaas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserStatusDua",
                table: "UserAccesses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserStatusDua",
                table: "UserAccesses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
