using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectsManager.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnused : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamLeaderEmployeeId",
                table: "Projects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamLeaderEmployeeId",
                table: "Projects",
                type: "int",
                nullable: true);
        }
    }
}
